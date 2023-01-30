using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (ModelState.IsValid) {
                _db.Category.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return View(category);
            }
        }

        // EDIT.
        public IActionResult Edit(int? idCategory)
        {
            if (idCategory == null || idCategory == 0)
            {
                return NotFound();
            }

            var obj = _db.Category.Find(idCategory);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        // DELETE.
        public IActionResult Delete(int? idCategory)
        {
            if (idCategory == null || idCategory == 0)
            {
                return NotFound();
            }

            var obj = _db.Category.Find(idCategory);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _db.Category.Remove(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
