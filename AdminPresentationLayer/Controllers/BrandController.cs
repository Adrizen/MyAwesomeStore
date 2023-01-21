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
            if (ModelState.IsValid)
            {
                _db.Brand.Add(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return View(brand);
            }

            
        }

        // Edit
        public IActionResult Edit(int? idBrand)
        {
            if (idBrand == null || idBrand == 0)
            {
                return NotFound();
            }

            var obj = _db.Brand.Find(idBrand);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brand.Update(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(brand);
            }
        }

        // Delete
        public IActionResult Delete(int? idBrand)
        {
            if (idBrand == null || idBrand == 0)
            {
                return NotFound();
            }

            var obj = _db.Brand.Find(idBrand);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Brand brand)
        {
            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                _db.Brand.Remove(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}
