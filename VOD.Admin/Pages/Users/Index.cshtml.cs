using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using VOD.Database.Services;
using VOD.Common.DTOModels;


namespace VOD.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        #region Properties
        private readonly IUserService _userService;

        public IEnumerable<UserDTO> Users = new List<UserDTO>();
       
        [TempData] 
        public string Alert { get; set; }
        #endregion


        #region Constructor 
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion


        public async Task OnGetAsync()
        {
            Users = await _userService.GetUsersAsync();
        }

    }
}
