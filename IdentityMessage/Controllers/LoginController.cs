using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace IdentityMessage.Controllers;
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var identity = await _userManager.CreateAsync(new AppUser
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.UserName,

            }, model.ConfirmPassword);

            if (identity.Succeeded)
            {
                TempData["MessageSuccess"] = "Üyelik kayıt işlemi gerçekleştirilmiştir";
                return RedirectToAction("SignUp");
            }
            foreach (IdentityError item in identity.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View();

        }
        return View();
      
    }
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel p)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Mail");
            }
            else
            {
                ViewBag.ErrorMessage = "Geçersiz Kullanıcı adı veya Şifre. Lütfen tekrar deneyiniz.";
                return View();
            }
        }
        else
        {
            ViewBag.ErrorMessage = "Lütfen giriş bilgilerinizi doğru şekilde doldurunuz.";
            return View();
        }
    }
    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Default");
    }
}
