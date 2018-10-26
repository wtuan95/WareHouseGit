﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WareHouse.Models
{
    public class UniqueNameNew: ValidationAttribute
    {
        private readonly hotellte_warehouseEntities hotellte_warehouseEntities = new hotellte_warehouseEntities();

        public override bool IsValid(object value)
        {
            return hotellte_warehouseEntities.News.SingleOrDefault(m => m.Alias_SEO == value.ToString()) == null;
        }
    }
}