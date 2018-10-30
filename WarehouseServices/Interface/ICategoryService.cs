﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;


namespace Warehouse.Services.Interface
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int Id);
        Category GetByAlias(string alias);
    }
}
