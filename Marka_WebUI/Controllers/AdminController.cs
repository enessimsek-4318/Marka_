﻿using Marka_BLL.Abstract;
using Marka_Entity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
            var product = _productService.GetByIdWithCategories(id);
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
                Id = product.Id,
                SelectedCategories=product.ProductCategories.Select(i=>i.Category).ToList()
            };
            ViewBag.Categories=_categoryService.GetAll();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model,List<IFormFile> files, int[] categoryIds)
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
            _productService.Update(entity,categoryIds);
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
        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories=_categoryService.GetAll()
            });
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity=new Category()
            {
                Name=model.CategoryName,
            };
            _categoryService.Create(entity);
            return RedirectToAction("CategoryList");
        }
        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithProducts(id);
            if (entity!=null)
            {
                return View(new CategoryModel()
                {
                    Id = entity.Id,
                    CategoryName = entity.Name,
                    Products = entity.ProductCategories.Select(i => i.Product).ToList()
                });
            }
            else
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity=_categoryService.GetById(model.Id);
            if (entity==null)
            {
                return NotFound();
            }
            entity.Name = model.CategoryName;
            _categoryService.Update(entity);
            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var entity = _categoryService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            _categoryService.Delete(entity);
            return RedirectToAction("CategoryList");
        }
    }
}
