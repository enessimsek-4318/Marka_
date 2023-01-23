using Marka_BLL.Abstract;
using Marka_Entity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("products/{category?}")]
        public IActionResult List(string category,int page=1)
        {
            const int pageSize = 6;
            return View(new ProductListModel()
            {
                Products = _productService.GetProductsByCategory(category,page,pageSize)
            });
        }
        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Product product=_productService.GetProductDetails(id.Value);
            if (product==null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            }) ;
        }
    }
}
