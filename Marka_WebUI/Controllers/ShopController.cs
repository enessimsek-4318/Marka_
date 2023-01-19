using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
