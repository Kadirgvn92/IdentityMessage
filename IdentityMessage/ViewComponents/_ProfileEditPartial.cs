using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessage.ViewComponents;

public class _ProfileEditPartial : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public _ProfileEditPartial(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        return View();
    }
}
