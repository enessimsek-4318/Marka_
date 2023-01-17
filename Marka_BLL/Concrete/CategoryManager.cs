using Marka_BLL.Abstract;
using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marka_BLL.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryService _categoryService;
        public CategoryManager(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Create(Category entity)
        {
            _categoryService.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryService.Delete(entity);
        }

        public Category Find(Expression<Func<Category, bool>> filter)
        {
            return _categoryService.Find(filter);
        }

        public IEnumerable<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryService.GetAll(filter);
        }

        public Category GetById(int id)
        {
            return _categoryService.GetById(id);
        }

        public void Update(Category entity)
        {
            _categoryService.Update(entity);
        }
    }
}
