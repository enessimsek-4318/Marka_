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

        public void InitializeCart(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }
    }
}
