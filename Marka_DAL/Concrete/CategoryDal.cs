﻿using Marka_DAL.Abstract;
using Marka_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marka_DAL.Concrete
{
    internal class CategoryDal : GenericRepository<Category,DataContext>,ICategoryDal
    {

    }
}
