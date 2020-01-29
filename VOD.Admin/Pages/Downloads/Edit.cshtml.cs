using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using VOD.Database.Services;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Services;
using VOD.Common.Entities;
using VOD.Common.Extensions;

namespace VOD.Admin.Pages.Downloads
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        #region Properties 
        private readonly IAdminService _db;
        [BindProperty]
        public DownloadDTO Input { get; set; } =
            new DownloadDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor 
        public EditModel(IAdminService db)
        {
            _db = db;
        }
        #endregion

        #region Actions 
        public async Task<IActionResult> OnGetAsync(int id, int courseId,
        int moduleId)
        {
            try
            {
                ViewData["Modules"] = (await _db.GetAsync<Module,
                    ModuleDTO>(true)).ToSelectList("Id", "CourseAndModule");
                Input = await _db.SingleAsync<Download, DownloadDTO>(s =>
                    s.Id.Equals(id) && s.ModuleId.Equals(moduleId) &&
                    s.CourseId.Equals(courseId), true);
                return Page();
            }
            catch
            {
                return RedirectToPage("/Index", new
                {
                    alert = "You do not have access to this page."
                });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var id = Input.ModuleId;
                Input.CourseId = (await _db.SingleAsync<Module, ModuleDTO>(
                    s => s.Id.Equals(id) && s.CourseId.Equals(0))).CourseId;
                var succeeded = await _db.UpdateAsync<DownloadDTO,
                              Download>(Input);

                if (succeeded)
                {
                    // Message sent back to the Index Razor Page. 
                    Alert = $"Updated Download: {Input.Title}.";

                    return RedirectToPage("Index");
                }
            }

            // Something failed, redisplay the form. 
            // Reload the modules when the page is reloaded 
            ViewData["Modules"] = (await _db.GetAsync<Module,
                ModuleDTO>(true)).ToSelectList("Id", "CourseAndModule");

            return Page();
        }
        #endregion
    }


}