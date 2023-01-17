using Marka_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Concrete
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context=new DataContext();
            if (context.Database.GetPendingMigrations().Count() == 0)    
            {
                if (context.Categories.Count()==0)
                {
                    context.Categories.AddRange(Categories);
                }
                if (context.Products.Count()==0)
                {
                    context.Products.AddRange(Products);
                }
                context.SaveChanges();
            }
        }
        private static List<Category> Categories = new List<Category>()
        {
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Telefon"}
        };
        private static Product[] Products =
        {
            new Product(){Name="Iphone 14 Pro", Price=45000,Images={new Image(){ImageUrl="1.jpg"},new Image(){ImageUrl="2.jpg"}, new Image() { ImageUrl = "3.jpg" },new Image(){ImageUrl="4.jpg" } },Description="<p>Kamerası çok iyi</p>" },
            new Product(){Name="Iphone 14 Pro Max", Price=50000,Images={new Image(){ImageUrl="5.jpg"},new Image(){ImageUrl="6.jpg"}, new Image() { ImageUrl = "7.jpg" },new Image(){ImageUrl="8.jpg" } },Description="<p>Biz daha iyisini yapana kadar en iyisi</p>" },
            new Product(){Name="Iphone 14 ", Price=30000,Images={new Image(){ImageUrl="9.jpg"},new Image(){ImageUrl="10.jpg"}, new Image() { ImageUrl = "11.jpg" },new Image(){ImageUrl="12.jpg" } },Description="<p>Çok Hızlı</p>" },
            new Product(){Name="Iphone 14 Max", Price=32000,Images={new Image(){ImageUrl="13.jpg"},new Image(){ImageUrl="14.jpg"}, new Image() { ImageUrl = "15.jpg" },new Image(){ImageUrl="16.jpg" } },Description="<p>Mükemmel</p>" },
            new Product(){Name="Iphone 13 Pro", Price=38000,Images={new Image(){ImageUrl="17.jpg"},new Image(){ImageUrl="18.jpg"}, new Image() { ImageUrl = "19.jpg" },new Image(){ImageUrl="20.jpg" } },Description="<p>Şahane</p>" },
            new Product(){Name="Iphone 13 Pro Max", Price=41000,Images={new Image(){ImageUrl="21.jpg"},new Image(){ImageUrl="22.jpg"}, new Image() { ImageUrl = "23.jpg" },new Image(){ImageUrl="24.jpg" } },Description="<p>Güzellik</p>" },
            new Product(){Name="Iphone 13 ", Price=27000,Images={new Image(){ImageUrl="25.jpg"},new Image(){ImageUrl="26.jpg"}, new Image() { ImageUrl = "27.jpg" },new Image(){ImageUrl="28.jpg" } },Description="<p>Güçlü</p>" },
        };
    }
}
