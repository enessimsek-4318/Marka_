using Marka_BLL.Abstract;
using Marka_Entity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model, List<IFormFile> files)
        {
            ModelState.Remove("Images");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Product()
            {
                Name = model.ProductName,
                Price = model.Price,
                Description = model.Description
            };

            if (files != null)
            {
                foreach (var file in files)
                {
                    Image image = new Image();
                    image.ImageUrl = file.FileName;

                    entity.Images.Add(image);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); 
                    }
                }
            }

            _productService.Create(entity);

            return RedirectToAction("ProductList");
        }
        public IActionResult ProductList()
        {
            ProductListModel model = new ProductListModel()
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public IActionResult EditProduct(int id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var product = _productService.GetProductDetails(id);
            if (product == null)
            {
                return BadRequest();
            }
            var entity = new ProductModel()
            {
                ProductName = product.Name,
                Price = product.Price,
                Description = product.Description,
                Images = product.Images,
                Id = product.Id
            };
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model,List<IFormFile> files)
        {
            var entity = _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name=model.ProductName;
            entity.Price=model.Price;
            entity.Description=model.Description;
            if (files != null)
            {
                foreach (var file in files)
                {
                    Image image = new Image();
                    image.ImageUrl = file.FileName;

                    entity.Images.Add(image);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            _productService.Update(entity);
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var entity= _productService.GetById(productId);
            if (entity!=null)
            {
                _productService.Delete(entity);
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("ProductList");
        }
    }
}
