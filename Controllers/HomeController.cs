using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp.netcoreIdentityApp.Web.Models;
using asp.netcoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using asp.netcoreIdentityApp.Web.Extensions;
using asp.netcoreIdentityApp.Web.Services;

namespace asp.netcoreIdentityApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<AppUser> _Usermanager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailService _emailservice;

    public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signIn, IEmailService emailservice)
    {
        _Usermanager = userManager;
        _signInManager = signIn;
        _emailservice = emailservice;
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
    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
    {
        var hasUser = await _Usermanager.FindByEmailAsync(request.Email);

        if (hasUser == null)
        {
            ModelState.AddModelError(string.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır.");
            return View();

        }
        string passwordResetToken = await _Usermanager.GeneratePasswordResetTokenAsync(hasUser);

        var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken });

        //email service de kaldık
        await _emailservice.SendResetPasswordEmail(passwordResetLink,hasUser.Email);



        TempData["Success"] = "Şifre yenileme linki, eposta adresinize gönderilmiştir";

        return RedirectToAction(nameof(ForgetPassword));
    }


    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
    {
        returnUrl = returnUrl ?? Url.Action("Index", "Home");

        var hasUser = await _Usermanager.FindByEmailAsync(model.Email);

        if (hasUser == null)
        {
            ModelState.AddModelError(string.Empty, "Email veyaşifre yanlış. ");
            return View();
        }

        var signInresult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

        if (signInresult.Succeeded)
        {
            return Redirect(returnUrl);
        }

        if (signInresult.IsLockedOut)
        {
            ModelState.AddModelErrorList(new List<string>() { "3 dk sonra tekrar deneyiniz ." });
            return View(model);

        }

        ModelState.AddModelErrorList(new List<string>() { "Email veya şifre yanlış." });

        return View(model);
    }



    [HttpPost]
    public async Task<IActionResult> SingUp(SignUpViewModel request)
    {

        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var identityResult = await _Usermanager.CreateAsync(new()
        {
            UserName = request.UserName,
            PhoneNumber = request.Phone,
            Email = request.Email
        }, request.Password);



        if (identityResult.Succeeded)
        {
            TempData["SuccessMessage"] = "Üyelik işlemi başarıyla tmamlandı :)";
            return RedirectToAction(nameof(HomeController.SingUp));
        }

        ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());


        return View(request);

    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}