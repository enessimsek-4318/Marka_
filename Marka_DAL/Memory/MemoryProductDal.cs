using Marka_DAL.Abstract;
using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Memory
{
    public class MemoryProductDal //: IProductDal
    {
        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Find(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            var products = new List<Product>()
            {
                new Product(){Id=1,Name="Iphone 14",Price=32000,Images={ new Image(){ImageUrl="1.jpg" } } },
                new Product(){Id=1,Name="Iphone 13",Price=30000,Images={ new Image(){ImageUrl="2.jpg" } } },
                new Product(){Id=1,Name="Iphone 12",Price=27000,Images={ new Image(){ImageUrl="3.jpg" } } },
            };
            return products;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPopularProduct()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
