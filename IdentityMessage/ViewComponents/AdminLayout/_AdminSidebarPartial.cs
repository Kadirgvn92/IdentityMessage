using IdentityMessage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalYoutube.PresentationLayer.ViewComponents.AdminLayout;

public class _AdminSidebarPartial : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;

    public _AdminSidebarPartial(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = User.Identity.Name;
        if(user != null)
        {
            var values = await _userManager.FindByNameAsync(user);
            return View(values);
        }
        return View();
        
    }
}
