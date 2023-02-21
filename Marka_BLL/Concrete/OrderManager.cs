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
    public class OrderManager:IOrderService
    {
        private IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal=orderDal;
        }

        public void Create(Order entity)
        {
            _orderDal.Create(entity);
        }
    }
}
