using Microsoft.AspNetCore.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
