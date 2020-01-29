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
    public class CreateModel : PageModel
    {
        #region Properties 
        private readonly IAdminService _db;
        [BindProperty]
        public DownloadDTO Input { get; set; } =
               new DownloadDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor 
        public CreateModel(IAdminService db)
        {
            _db = db;
        }
        #endregion

        #region Actions 
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                ViewData["Modules"] = (await _db.GetAsync<Module,
                    ModuleDTO>(true)).ToSelectList("Id", "CourseAndModule");
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
                var succeeded = (await _db.CreateAsync<DownloadDTO,
                    Download>(Input)) > 0;

                if (succeeded)
                {
                    // Message sent back to the Index Razor Page. 
                    Alert = $"Created a new Download: {Input.Title}.";

                    return RedirectToPage("Index");
                }
            }


            // Something failed, redisplay the form. 
            ViewData["Modules"] = (await _db.GetAsync<Module,
                ModuleDTO>(true)).ToSelectList("Id", "CourseAndModule");
            return Page();
        }
        #endregion
    }
}



