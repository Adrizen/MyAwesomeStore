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

                // TODO: Please note that trying to load an image with BRAVE BROWSER crash the project.
                if (productVM.product.idProduct == 0)
                {
                    // Create a new product with a new image.
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
                    // Update product's image.
                    var objProduct = _db.Products.AsNoTracking().FirstOrDefault(p => p.idProduct == productVM.product.idProduct);

                    if (files.Count > 0)    // A new image was loaded.
                    {
                        string upload = webRootPath + WC.imageURL;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        // Delete previous image.
                        var oldImageFile = Path.Combine(upload, objProduct.imageURL);
                        if (System.IO.File.Exists(oldImageFile))
                        {
                            System.IO.File.Delete(oldImageFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.product.imageURL = fileName + extension;
                    } else
                    {
                        // Update the product but not his image.
                        productVM.product.imageURL = objProduct.imageURL;
                    }
                    _db.Products.Update(productVM.product);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            } // End if ModelIsValid

            // Lists are filled again in case something fails.
            productVM.categoryList = _db.Category.Select(c => new SelectListItem
            {
                Text = c.info,
                Value = c.idCategory.ToString()
            });
            productVM.brandList = _db.Brand.Select(c => new SelectListItem
            {
                Text = c.info,
                Value = c.idBrand.ToString()
            });

            return View(productVM);
        }

        // GET
        public IActionResult Delete(int? idProduct)
        {
            if (idProduct == null || idProduct == 0)
            {
                return NotFound();
            }

            Product product = _db.Products.Include(c => c.Category)
                                          .Include(b => b.Brand)
                                          .FirstOrDefault(p => p.idProduct == idProduct);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            // Delete the image.
            string upload = _webHostEnvironment.WebRootPath + WC.imageURL;

            // Delete previous image.
            var oldImageFile = Path.Combine(upload, product.imageURL);
            if (System.IO.File.Exists(oldImageFile))
            {
                System.IO.File.Delete(oldImageFile);
            }

            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
