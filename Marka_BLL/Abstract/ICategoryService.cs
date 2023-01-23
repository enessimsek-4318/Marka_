using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marka_BLL.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        Category Find(Expression<Func<Category, bool>> filter);
        List<Category> GetAll(Expression<Func<Category, bool>> filter = null);
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
