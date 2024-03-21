using Microsoft.AspNetCore.Mvc;

namespace TraversalYoutube.PresentationLayer.ViewComponents.AdminLayout;

public class _AdminFooterPartial :ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();  
    }
}
