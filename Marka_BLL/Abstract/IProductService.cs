using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Marka_Entity;

namespace Marka_BLL.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        Product Find(Expression<Func<Product, bool>> filter);
        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        

    }
}
