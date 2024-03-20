using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessage.Controllers;
public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SignUp(SignUpViewModel model)
    {
        return View();
    }
}
