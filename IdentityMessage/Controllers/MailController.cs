using Microsoft.AspNetCore.Mvc;

namespace IdentityMessage.Controllers;
public class MailController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
