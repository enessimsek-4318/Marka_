using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
