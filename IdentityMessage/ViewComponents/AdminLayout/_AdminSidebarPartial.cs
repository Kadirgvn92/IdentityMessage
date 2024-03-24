using IdentityMessage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TraversalYoutube.PresentationLayer.ViewComponents.AdminLayout;

public class _AdminSidebarPartial : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public _AdminSidebarPartial(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = User.Identity.Name;
        if(user != null)
        {
            var values = await _userManager.FindByNameAsync(user);

            ViewBag.Index = _context.Mails.Where(x => x.ToUserEmail == values.Email && !x.IsDraft && !x.IsTrash).Count();

            ViewBag.DrafList = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == values.Email && x.IsDraft && !x.IsTrash).Count();

            ViewBag.Send = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == values.Email && !x.IsTrash && !x.IsDraft).Count();

            ViewBag.Important = _context.Mails.Include(x => x.AppUser).Where(x => x.IsImportant && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count();

            return View(values);
        }
        return View();
        
    }
}
