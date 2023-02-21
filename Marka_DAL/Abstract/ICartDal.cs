using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Abstract
{
    public interface ICartDal:IRepository<Cart>
    {
        Cart GetCartByUserId(string userId);
        void DeleteFromCart(int cartId, int productId);
        void ClearCart(string cartId);
    }
}
