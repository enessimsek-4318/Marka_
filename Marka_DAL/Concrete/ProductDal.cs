using Marka_DAL.Abstract;
using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Concrete
{
    public class ProductDal : GenericRepository<Product, DataContext>, IProductDal
    {
        public IEnumerable<Product> GetPopularProduct()
        {
            throw new NotImplementedException();
        }
    }
}

 