using Marka_BLL.Abstract;
using Marka_DAL.Abstract;
using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marka_BLL.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(string UserId, int productId, int quantity)
        {
            var cart=GetCartByUserId(UserId);
            if (cart!=null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index<0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity = quantity;
                }
                _cartDal.Update(cart);
            }
        }

        public void ClearCart(string cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string UserId, int productId)
        {
            var cart=GetCartByUserId(UserId);
            if (cart!=null)
            {
                _cartDal.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }
    }
}
