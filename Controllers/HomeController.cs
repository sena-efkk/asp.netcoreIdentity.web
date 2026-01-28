using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp.netcoreIdentityApp.Web.Models;
using asp.netcoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace asp.netcoreIdentityApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<AppUser> _Usermanager;

    public HomeController(UserManager<AppUser> u)
    {
        _Usermanager=u;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult SingUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SingUp(SignUpViewModel request)
    {

        var identityResult =await _Usermanager.CreateAsync(new() {
                UserName=request.UserName ,PhoneNumber=request.Phone,Email=request.Email},request.PasswordConfirm);
        if (identityResult.Succeeded)
        {
            ViewBag.SuccessMessage="Üyelik işlemi başarıyla tmamlandı :)";
            return View();
        }

        foreach(IdentityError item in identityResult.Errors)
        {
            ModelState.AddModelError(string.Empty,item.Description);
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}