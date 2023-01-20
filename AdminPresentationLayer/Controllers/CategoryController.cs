using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Category;

            return View(categoryList);
        }

        // GET
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
