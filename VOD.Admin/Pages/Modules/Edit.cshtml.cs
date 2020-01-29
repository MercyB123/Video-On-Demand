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

namespace VOD.Admin.Pages.Modules
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        #region Properties 
        private readonly IAdminService _db;
        [BindProperty]
        public ModuleDTO Input { get; set; } = new
            ModuleDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor 
        public EditModel(IAdminService db)
        {
            _db = db;
        }
        #endregion
        #region Actions 
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                ViewData["Courses"] = (await _db.GetAsync<Course,
  CourseDTO>()).ToSelectList("Id", "Title");
                Input = await _db.SingleAsync<Module, ModuleDTO>(s =>
                    s.Id.Equals(id));
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
                var succeeded = await _db.UpdateAsync<ModuleDTO,
                    Module>(Input);

                if (succeeded)
                {
                    // Message sent back to the Index Razor Page. 
                    Alert = $"Updated Module: {Input.Title}.";

                    return RedirectToPage("Index");
                }
            }

            // Something failed, redisplay the form. 
            // Reload the modules when the page is reloaded 
            ViewData["Courses"] = (await _db.GetAsync<Course,
                CourseDTO>()).ToSelectList("Id", "Title");

            return Page();
        }
        #endregion
    }


}