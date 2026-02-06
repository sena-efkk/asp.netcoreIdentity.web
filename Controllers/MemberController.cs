using asp.netcoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp.netcoreIdentityApp.Web.Controllers
{  
    [Authorize]
    public class MemberController :Controller
    {
        private readonly SignInManager<AppUser> _signinM;
        public MemberController(SignInManager<AppUser> sm)
        {
            this._signinM=sm;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task Logout()
        {
            await  _signinM.SignOutAsync();
        }
    }
    
}