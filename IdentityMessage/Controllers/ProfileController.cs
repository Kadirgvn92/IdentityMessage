using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace IdentityMessage.Controllers;
[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;

    public ProfileController(UserManager<AppUser> userManager, AppDbContext context, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Index()
    {

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(PasswordChangeViewModel model)
    {
        var hasUser = await _userManager.FindByNameAsync(User.Identity.Name);

        if (!ModelState.IsValid)
        {
            return View();
        }

        var checkOldPassword = await _userManager.CheckPasswordAsync(hasUser, model.OldPassword); //burada eski şifreyi check yaptırıyoruz.

        if (!checkOldPassword)
        {
            ModelState.AddModelError(string.Empty, "Eski şifreniz yanlıştır.");
        }

        var result = await _userManager.ChangePasswordAsync(hasUser, model.OldPassword, model.NewPassword);//Yeni şifreyle dğeiştiriyoruz.

        if (!result.Succeeded)
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
                return View();
            }
        }

        await _userManager.UpdateSecurityStampAsync(hasUser); //nurada şifremizi dğeiştirdikten sonra security stamp güncelledik.
        await _signInManager.SignOutAsync(); //kulllanıcıya yeni şifresiyle girmesi için çıkış yaptırdık 
        await _signInManager.PasswordSignInAsync(hasUser, model.NewPassword, true, false); //kullanıcıya yeni şifresiyle sisteme giriş yaptırdık.

        TempData["PasswordChange"] = "Şifreniz başarıyla değiştirilmiştir.";


        return View();

    }

}
