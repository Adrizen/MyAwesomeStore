using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _db.Products.Include(c => c.Category).Include(b => b.Brand);

            return View(productsList);
        }

        // Get.
        public IActionResult Upsert(int? idProduct)
        {
            Product product = new Product();
            if (idProduct == null)
            {
                // Create new product.
                return View(product);
            } else
            {
                product = _db.Products.Find(idProduct);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }
    }
}
