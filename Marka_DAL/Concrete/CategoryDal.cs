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
    internal class CategoryDal : ICategoryDal
    {

        public void Create(Category entity)
        {

            
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category Find(Expression<Func<Category, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
