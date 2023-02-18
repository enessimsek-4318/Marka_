using Marka_BLL.Abstract;
using Marka_Entity;
using Marka_WebUI.Identity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<ApplicationUser> _userManager;
        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService=cartService;
            _userManager=userManager;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    ImageUrl = i.Product.Images[0].ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int productId,int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User),productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User),productId);
            return RedirectToAction("Index");
        }
    }
}
