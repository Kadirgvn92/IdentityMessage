using IdentityMessage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TraversalYoutube.PresentationLayer.ViewComponents.AdminLayout;

public class _AdminHeaderPartial : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public _AdminHeaderPartial(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = User.Identity.Name;
        if (user != null)
        {
            var values = await _userManager.FindByNameAsync(user);

            ViewBag.Index = _context.Mails.Where(x => x.ToUserEmail == values.Email && !x.IsDraft && !x.IsTrash).Count();

            return View(values);
        }
        return View();
    }
}
