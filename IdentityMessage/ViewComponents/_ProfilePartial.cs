using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMessage.ViewComponents;

public class _ProfilePartial : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public _ProfilePartial(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);


        var model = new UserProfileViewModel()
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Inbox = _context.Mails.Include(x => x.AppUser).Where(x => x.ToUserEmail == user.Email && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count(),
            Send = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash && !x.IsDraft).Count(),
            UnRead = _context.Mails.Include(x => x.AppUser).Where(x => x.ToUserEmail == user.Email && !x.IsRead && !x.IsDraft).Count()
        };

        return View(model);
    }
}
