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

namespace VOD.Admin.Pages.Videos
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        #region Properties 
        private readonly IAdminService _db;
        public IEnumerable<VideoDTO> Items = new List<VideoDTO>();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor 
        public IndexModel(IAdminService db)
        {
            _db = db;
        }
        #endregion

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Items = await _db.GetAsync<Video, VideoDTO>(true);
                return Page();
            }
            catch
            {
                Alert = "You do not have access to this page.";
                return RedirectToPage("/Index");
            }

        }
    }
}