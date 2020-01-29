using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VOD.UI.Models;
using VOD.Common.Entities;
using Microsoft.AspNetCore.Identity;
using VOD.Database.Services;
using VOD.Common.Extensions;
using VOD.UI.Services;



namespace VOD.UI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IDbReadService _db;
        

        private readonly ILogger<HomeController> _logger;

        private SignInManager<VODUser> _signInManager;


        //constructor
        public HomeController(ILogger<HomeController> logger, SignInManager<VODUser> signInMgr/*, IDbReadService db */)
        {
            _logger = logger;
            _signInManager = signInMgr;
           
            

        }


        //Check if the user is signed in
        public IActionResult Index()
        {
            //var result1 = await _db.GetAsync<Download>(); // Fetch all 
            //                                              // Fetch all that matches the Lambda expression 
            //var result2 = await _db.GetAsync<Download>(d =>
            //    d.ModuleId.Equals(1));

            //_db.Include<Download>();
            //var result5 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));

            //_db.Include<Download>();
            //_db.Include<Module, Course>();
            //var result6 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));


            //var result3 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));

            //var result4 = await _db.AnyAsync<Download>(d =>d.ModuleId.Equals(1)); // True if a record is found 
            //var videos = new List<Video>();
            //var convertedVideos = videos.ToSelectList<Video>("Id", "Title");

            var user =  _signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                //var courses = await _db.GetCoursesAsync(user.Id);
                //var course = await _db.GetCourseAsync(user.Id, 1);
                //var videos = await _db.GetVideosAsync(user.Id, 1);
                //var video = await _db.GetVideoAsync(user.Id, 1);

            }

            if (!_signInManager.IsSignedIn(User))
                return RedirectToPage("/Account/Login",
                    new { Area = "Identity" });

            //var videos = await _db.GetVideosAsync(user.Id, 1);
           // var video = await _db.GetVideoAsync(user.Id, 1);


            //return View();

            return RedirectToAction("Dashboard", "Membership");


        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
