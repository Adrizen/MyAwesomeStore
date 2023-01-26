using AdminPresentationLayer.Data;
using AdminPresentationLayer.Models;
using AdminPresentationLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AdminPresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.product.idProduct == 0)
                {
                    // Create.
                    Console.WriteLine("Turip");
                    string upload = webRootPath + WC.imageURL;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.product.imageURL = fileName + extension;
                    _db.Products.Add(productVM.product);

                } else
                {
                    // Update.
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

    }
}
