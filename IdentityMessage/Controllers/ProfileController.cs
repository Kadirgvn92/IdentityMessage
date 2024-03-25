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
    public async Task<IActionResult> Index(ProfileViewModel model)
    {
        var hasUser = await _userManager.FindByNameAsync(User.Identity.Name);

        if (!ModelState.IsValid)
        {
            return View();
        }

        var checkOldPassword = await _userManager.CheckPasswordAsync(hasUser, model.PasswordChangeViewModel.OldPassword); //burada eski şifreyi check yaptırıyoruz.

        if(!checkOldPassword)
        {
            ModelState.AddModelError(string.Empty, "Eski şifreniz yanlıştır.");
        }

        var result = await _userManager.ChangePasswordAsync(hasUser, model.PasswordChangeViewModel.OldPassword, model.PasswordChangeViewModel.NewPassword);//Yeni şifreyle dğeiştiriyoruz.

        if(!result.Succeeded)
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
                return View();
            }
        }

        await _userManager.UpdateSecurityStampAsync(hasUser); //nurada şifremizi dğeiştirdikten sonra security stamp güncelledik.
        await _signInManager.SignOutAsync(); //kulllanıcıya yeni şifresiyle girmesi için çıkış yaptırdık 
        await _signInManager.PasswordSignInAsync(hasUser, model.PasswordChangeViewModel.NewPassword, true, false); //kullanıcıya yeni şifresiyle sisteme giriş yaptırdık.

        TempData["PasswordChange"] = "Şifreniz başarıyla değiştirilmiştir.";

        ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender))); 

        UserEditViewModel userEditViewModel = new UserEditViewModel();
        userEditViewModel.Username = hasUser.UserName;
        userEditViewModel.Email = hasUser.Email;
        userEditViewModel.Phone = hasUser.PhoneNumber;
        userEditViewModel.BirthDate = hasUser.BirthDate;
        userEditViewModel.Gender = hasUser.Gender;
        return View(userEditViewModel);

    }
    [HttpPost]
    public async Task<IActionResult> UserEdit(ProfileViewModel model)   
    {
        var hasUser = await _userManager.FindByNameAsync(User.Identity.Name);

        if(model.UserEditViewModel.ImageUrl != null)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(model.UserEditViewModel.Image.FileName);
            var imagename = Guid.NewGuid() + extension;
            var savelocation = resource + "/wwwroot/userimages/" + imagename;
            var stream = new FileStream(savelocation, FileMode.Create);
            await model.UserEditViewModel.Image.CopyToAsync(stream);
            model.UserEditViewModel.ImageUrl = imagename;
        }

        hasUser.Email = model.UserEditViewModel.Email;
        hasUser.UserName = model.UserEditViewModel.Username;
        hasUser.PhoneNumber = model.UserEditViewModel.Phone;
        hasUser.BirthDate = model.UserEditViewModel.BirthDate;
        hasUser.ImageUrl = model.UserEditViewModel.ImageUrl;

        //burada kaldım


        if (!ModelState.IsValid)
        {
            return View();
        }
        return View();

    }
}
