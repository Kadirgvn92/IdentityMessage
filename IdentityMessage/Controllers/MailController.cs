﻿using IdentityMessage.Models;
using IdentityMessage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

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
                TotalItems = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsTrash).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.ToUserEmail == user.Email && !x.IsTrash).Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(x => x.MailDate).ToList(),
            TotalMails = _context.Mails.Where(x => x.ToUserEmail == user.Email && !x.IsTrash).Count(),

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
    public async Task<IActionResult> CreateMail(int? id)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var draft = _context.Mails.FirstOrDefault(x => x.MailId == id);
        if (draft != null)
        {
            var draftMail = new MailViewModel()
            {
                MailId = draft.MailId,
                MailContent = draft.MailContent,
                MailSubject = draft.MailSubject,
                ToUserEmail = draft.ToUserEmail,
            };
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

            var deleteDraft = _context.Mails.FirstOrDefault(x => x.MailId == model.MailId);
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
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash).Count(),

        };
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
                TotalItems = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email).Count(),
                CurrentPage = page,
                ItemsPerPage = pageSize,
            },
            Mails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && x.IsDraft && !x.IsTrash).OrderByDescending(x => x.MailDate).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalMails = _context.Mails.Include(x => x.AppUser).Where(x => x.AppUser.Email == user.Email && !x.IsTrash).Count(),

        };
        return View(model);
    }
    [HttpGet]
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

        var deleteDraft = _context.Mails.FirstOrDefault(x => x.MailId == model.MailId);
        if (deleteDraft != null)
        {
            _context.Mails.Remove(deleteDraft);
            _context.SaveChanges();
        }
        return RedirectToAction("Index", "Mail");
    }
}

