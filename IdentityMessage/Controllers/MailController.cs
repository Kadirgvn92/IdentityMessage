using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessage.Controllers;
[Authorize]
public class MailController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public MailController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new InboxMailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsTrash).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsTrash).ToList(),
            TotalMails = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsTrash).Count(),

        };
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> MailDetail(int id)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var values = _context.Mails.Where(x => x.MailId == id).FirstOrDefault();
        MailDetailViewModel model = new MailDetailViewModel()
        {
            AppUserID = values.AppUserID,
            IsDraft = false,
            IsImportant = false,
            IsJunk = false,
            IsTrash = false,
            IsRead = true,
            ToUserEmail = values.ToUserEmail,
            MailContent = values.MailContent,
            MailDate = values.MailDate,
            MailId = values.MailId,
            MailSubject = values.MailSubject,
            MailTime = values.MailTime
        };
        return View(model);
    }
}
