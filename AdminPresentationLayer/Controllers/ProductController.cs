using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using AdminPresentationLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //IEnumerable<SelectListItem> categoryDropdown = _db.Category.Select(c => new SelectListItem
            //{
            //    Text = c.info,
            //    Value = c.idCategory.ToString()
            //});

            //ViewBag.categoryDropdown = categoryDropdown;

            //Product product = new Product();

            ProductVM productVM = new ProductVM()
            {
                product = new Product(),
                categoryList = _db.Category.Select(c => new SelectListItem
            {
                Text = c.info,
                Value = c.idCategory.ToString()
            }),
                brandList = _db.Brand.Select(c => new SelectListItem
                {
                    Text = c.info,
                    Value = c.idBrand.ToString()
                }),
            };

            if (idProduct == null)
            {
                // Create new product.
                return View(productVM);
            } else
            {
                productVM.product = _db.Products.Find(idProduct);
                if (productVM.product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }
    }
}
