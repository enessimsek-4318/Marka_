using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marka_BLL.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string UserId, int productId, int quantity);
        void DeleteFromCart(string UserId, int productId);
        void ClearCart(string cartId);
    }
}
