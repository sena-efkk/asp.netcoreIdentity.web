using asp.netcoreIdentityApp.Web.Areas.Admin.Models;
using asp.netcoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.netcoreIdentityApp.Web.Areas.Admin.Controllers // Namespace'e dikkat!
{

    [Area("Admin")] // <--- BU OLMADAN ÇALIŞMAZ! Klasör adıyla birebir aynı olmalı.
    public class HomeController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> UserList()
        {
            var userList= await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x=> new UserViewModel()
            {
                Id=x.Id,
                Name=x.UserName,
                Email=x.Email
            }).ToList();

            return View(userViewModelList);
        }
    }
}