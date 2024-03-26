using IdentityMessage.Models;
using IdentityMessage.Services;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Principal;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace IdentityMessage.Controllers;
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailService _emailService;

    public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
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

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Login");
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

        var passwordResetLink = Url.Action("ResetPassword", "Login", new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);

        //Email Service
        await _emailService.SendResetEmail(passwordResetLink, hasUser.Email);


        TempData["ResetSuccess"] = "Şifre sıfırlama linki mail adresinize gönderilmiştir.";


        return RedirectToAction("ForgetPassword");
    }

    [HttpGet]
    public async Task<IActionResult> ResetPassword(string userId, string token)
    {
        TempData["userId"] = userId;
        TempData["token"] = token;


        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        var userId = TempData["userId"].ToString();
        var token = TempData["token"].ToString();

        var hasUser = await _userManager.FindByIdAsync(userId);
        if (hasUser == null)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamamıştır.");
            return View();
        }

        var result = await _userManager.ResetPasswordAsync(hasUser, token, model.Password);

        if (result.Succeeded)
        {
            TempData["Reset"] = "Şifreniz yenilenmiştir.";
        }
        else
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
        }
        return View();
    }

    public IActionResult SignInGoogle(string ReturnUrl)
    {
        string RedirectUrl = Url.Action("ExternalResponse", "Login", new { ReturnUrl = ReturnUrl });

        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", RedirectUrl);

        return new ChallengeResult("Google", properties);
    }

    public IActionResult SignInFacebook(string ReturnUrl)
    {
        string RedirectUrl = Url.Action("ExternalResponse", "Login", new { ReturnUrl = ReturnUrl });

        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", RedirectUrl);

        return new ChallengeResult("Facebook", properties);
    }

    public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
    {
        ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction("SignIn");
        }
        else
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Mail");
            }
            else
            {
                AppUser user = new AppUser();
                user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                

                if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
                {
                    string userName = info.Principal.FindFirst(ClaimTypes.Surname).Value;
                    string normalizedUserName = userName.Normalize(System.Text.NormalizationForm.FormD);
                    Regex regex = new Regex("[^a-zA-Z0-9]");
                    string alphanumericUserName = regex.Replace(normalizedUserName, "");
                    userName = alphanumericUserName.ToLower() + ExternalUserId.Substring(0, 5).ToString();
                    user.UserName = userName;
                }
                else
                {
                    user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                }
                IdentityResult createResult = await _userManager.CreateAsync(user);

                if (createResult.Succeeded)
                {
                    IdentityResult loginResult = await _userManager.AddLoginAsync(user, info); //Identityde Login tablosunu doldurmak için yaptık
                    if (loginResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        return RedirectToAction("Index", "Mail");
                    }
                }
                else
                {
                    foreach (var item in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, "Google girişi yapılamadı.Lütfen tekrar deneyiniz.");
                    }
                }

            }
            return RedirectToAction("SignIn");
        }


    }
}



