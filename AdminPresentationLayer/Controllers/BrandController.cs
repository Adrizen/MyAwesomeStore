using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Brand> brandList = _db.Brand;

            return View(brandList);
        }

        // GET
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            _db.Brand.Add(brand);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
