using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.ToUserEmail == user.Email && !x.IsDraft && !x.IsTrash && !x.IsJunk).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count(),

        };
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> MailDetail(int id)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var values = _context.Mails.Where(x => x.MailId == id).FirstOrDefault();
        values.IsRead = true;
        _context.SaveChanges();
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
            MailTime = values.MailTime,
            AppUser = user

        };

        TempData["MailDetail"] = "bg-light";

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DraftList(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && x.IsDraft && !x.IsTrash).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && x.IsDraft && !x.IsTrash).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && x.IsDraft && !x.IsTrash).Count(),

        };
        return View(model);
    }

    public async Task<IActionResult> Draft(MailViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);


        Mail newMail = new Mail()
        {
            AppUserID = user.Id,
            ToUserEmail = model.ToUserEmail,
            MailSubject = model.MailSubject,
            MailContent = model.MailContent,
            MailDate = DateTime.Now,
            MailTime = DateTime.Now.TimeOfDay,
            IsDraft = true,
            IsImportant = false,
            IsJunk = false,
            IsRead = true,
            IsTrash = false,
        };

        _context.Mails.Add(newMail);
        _context.SaveChanges();

        TempData["MessageSuccess"] = "Taslak başarıyla kaydedildi.";

        return RedirectToAction("CreateMail");
    }
    [HttpGet]
    public async Task<IActionResult> CreateMail(int? id)
    {

        TempData["message"] = TempData["MessageSuccess"];
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var draft = _context.Mails.FirstOrDefault(x => x.MailId == id && x.IsDraft);
        if (draft != null)
        {
            var draftMail = new MailViewModel()
            {
                MailId = draft.MailId,
                MailContent = draft.MailContent,
                MailSubject = draft.MailSubject,
                ToUserEmail = draft.ToUserEmail,
            };
            _context.SaveChanges();
            return View(draftMail);
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateMail(MailViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);


        if (ModelState.IsValid)
        {
            Mail newMail = new Mail()
            {
                AppUserID = user.Id,
                ToUserEmail = model.ToUserEmail,
                MailSubject = model.MailSubject,
                MailContent = model.MailContent,
                MailDate = DateTime.Now,
                MailTime = DateTime.Now.TimeOfDay,
                IsDraft = false,
                IsImportant = false,
                IsJunk = false,
                IsRead = false,
                IsTrash = false,
            };
            if (model.ToUserEmail == user.Email)
            {
                ViewBag.EmailError = "Kendinize mail gönderemezsiniz";
                return View(model);
            }
            _context.Mails.Add(newMail);
            _context.SaveChanges();



            var deleteDraft = _context.Mails.FirstOrDefault(x => x.MailId == model.MailId && x.IsDraft);
            if (deleteDraft != null)
            {
                _context.Mails.Remove(deleteDraft);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Mail");
        }
        else
        {
            ViewBag.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

            return View(model);
        }

    }
    [HttpGet]
    public async Task<IActionResult> Send(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash && !x.IsDraft).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash && !x.IsDraft).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash && !x.IsDraft).Count(),

        };
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Important(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsImportant && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsImportant && !x.IsDraft && !x.IsTrash && !x.IsJunk).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsImportant && !x.IsDraft && !x.IsTrash && !x.IsJunk).Count(),

        };
        return View(model);
    }
    public async Task<IActionResult> MakeImportant(int id,string redirectAction)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var important = _context.Mails.Find(id);

        if (important.IsImportant)
        {
            important.IsImportant = false;
        }
        else
        {
            important.IsImportant = true;
        }
        _context.Mails.Update(important);
        _context.SaveChanges();
        return RedirectToAction(redirectAction);
    }

    [HttpGet]
    public async Task<IActionResult> Trash(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsTrash).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsTrash).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsTrash).Count(),

        };
        return View(model);
    }
    public async Task<IActionResult> MakeTrash(int id,string redirectAction)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var trash = _context.Mails.Find(id);

        trash.IsTrash = true;

        _context.Mails.Update(trash);
        _context.SaveChanges();
        return RedirectToAction(redirectAction);
    }

    public async Task<IActionResult> DeleteMail(int id)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var trash = _context.Mails.Find(id);

        _context.Mails.Remove(trash);
        _context.SaveChanges();
        return RedirectToAction("Trash");
    }
    public async Task<IActionResult> DeleteAllMail()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var trash = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsTrash || x.IsJunk).ToList();

        _context.Mails.RemoveRange(trash);
        _context.SaveChanges();
        return RedirectToAction("Trash");
    }

    [HttpGet]
    public async Task<IActionResult> Junk(int page = 1)
    {
        const int pageSize = 10;
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = new MailViewModel
        {
            PageInfo = new PageInfoModel()
            {
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsJunk && !x.IsDraft && !x.IsTrash && !x.IsImportant).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsJunk && !x.IsDraft && !x.IsTrash && !x.IsImportant).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => (x.AppUserID == user.Id || x.ToUserEmail == user.Email) && x.IsJunk && !x.IsDraft && !x.IsTrash && !x.IsImportant).Count(),

        };
        return View(model);
    }
    public async Task<IActionResult> MakeJunk(int id, string redirectAction)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var junk = _context.Mails.Find(id);

        junk.IsJunk = true;

        _context.Mails.Update(junk);
        _context.SaveChanges();
        return RedirectToAction(redirectAction);
    }
}

