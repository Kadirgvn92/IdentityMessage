using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessage.Controllers;
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

    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {

        var identity = await _userManager.CreateAsync(new AppUser
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            PhoneNumber = model.Phone,
            UserName = model.UserName,

        }, model.Password);

        if(identity.Succeeded)
        {
            TempData["MessageSuccess"] = "Üyelik kayıt işlemi gerçekleştirilmiştir";
            return RedirectToAction("SignUp");
        }

        foreach(IdentityError item in identity.Errors)
        {
            ModelState.AddModelError(string.Empty,item.Description);
        }
        return View();
    }
}
