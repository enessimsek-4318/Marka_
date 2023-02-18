﻿using Marka_DAL.Abstract;
using Marka_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Concrete
{
    public class CartDal : GenericRepository<Cart, DataContext>, ICartDal
    {
        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context=new DataContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd,cartId,productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context=new DataContext())
            {
                return context.Carts.Include(İ => İ.CartItems).ThenInclude(i => i.Product).ThenInclude(i=> i.Images).FirstOrDefault(i => i.UserId == userId);
            }
        }
    }
}
