using Marka_DAL.Abstract;
using Marka_Entity;
using Microsoft.EntityFrameworkCore;
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
        public new List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                return filter==null
                    ?context.Products.Include(i=> i.Images).ToList()
                    :context.Products.Include("Images").Where(filter).ToList();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new DataContext())
            {
                return context.Products.Where(i => i.Id == id).Include("Images").Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
            }
        }
    }
}
