﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Warehouse.Common;
using Warehouse.Core.DataAccess;
using Warehouse.Entities;

namespace Warehouse.Data.Interface
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        List<Category> SortList(List<Category> entities,ENUM.SORT_TYPE sortType = ENUM.SORT_TYPE.Descending);
    }
}
