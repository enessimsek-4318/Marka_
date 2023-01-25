using Marka_DAL.Abstract;
using Marka_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public int GetCountByCategory(string category)
        {
            using (var context=new DataContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                };
                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new DataContext())
            {
                return context.Products.Where(i => i.Id == id).Include("Images").Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category,int page,int pageSize)
        {
            using (var context = new DataContext())
            {
                var products = context.Products.Include("Images").AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(i => i.ProductCategories)
                                     .ThenInclude(i => i.Category)
                                     .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }
        public new void Update(Product entity)
        {
            using (var context=new DataContext())
            {
                var product=context.Products.FirstOrDefault(i => i.Id == entity.Id);
                if (product!=null)
                {
                    context.Images.RemoveRange(context.Images.Where(i => i.ProductId == entity.Id).ToList());
                    product.Name = entity.Name;
                    product.Description=entity.Description;
                    product.Price=entity.Price;
                    product.Images = entity.Images;
                }
                context.SaveChanges();
            }
        }
    }
}
