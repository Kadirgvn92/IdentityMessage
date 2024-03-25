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
            var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, p.RememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Mail");
            } 

            if (result.IsLockedOut) //giriş denemeleri için yanlış giriş teşebbüslerinde verilecek reaksiyonları yazabiliriz
            {
                ViewBag.ErrorMessage = "Çok fazla yanlış deneme yaptınız.\n 3 dakika boyunca giriş yapamazsınız.";
                return View();
            }
            var user = await _userManager.FindByNameAsync(p.Username);
            //Buradada kaç tane yanlış denemesi olduğunu gösteriyoruz.
            ViewBag.ErrorMessage = $"Geçersiz Kullanıcı adı veya Şifre. Lütfen tekrar deneyiniz.\n Başarısız Giriş Denemesi = {await _userManager.GetAccessFailedCountAsync(user)}";
            return View();

        }
        else
        {
            ViewBag.ErrorMessage = "Lütfen giriş bilgilerinizi doğru şekilde doldurunuz.";
            return View();
        }
    }

    public async Task LogOut() //Bu metotta program.cs'te cookie içerisine belirlenen url üzerinden hangi sayfada nereye çıkış yapabileceğimizi seçebileceğiz
    {
        await _signInManager.SignOutAsync();
    }
    [HttpGet]
    public IActionResult ForgetPassword()
    {
        return View();  
    }
    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
    {
        var hasUser = await _userManager.FindByEmailAsync(model.Email);
        if (hasUser == null)
        {
            ModelState.AddModelError(string.Empty, "Email adresine sahip kullanıcı bulunamamıştır.");
            return View();

        }
        string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

        var passwordResetLink = Url.Action("ResetPassword","Login", new { userId = hasUser.Id , Token = passwordResetToken });

        TempData["ResetSuccess"] = "Email adresine sahip kullanıcı bulunamamıştır.";

        return RedirectToAction("ForgetPassword");
    }





    //altta gösterilen sigout belirlenen adrese çıkış yapar 

    //[HttpGet]
    //public async Task<IActionResult> LogOut()
    //{
    //    await _signInManager.SignOutAsync();
    //    return RedirectToAction("SignIn", "Login");
    //}
}
