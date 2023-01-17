using Marka_BLL.Abstract;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {

            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }
    }
}
