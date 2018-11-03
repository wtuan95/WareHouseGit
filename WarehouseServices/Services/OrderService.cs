﻿using System;
using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Linq;
namespace Warehouse.Services.Services
{
    public class OrderService : IOrderService
    {
        private IOrderDal _orderDal;

        public OrderService(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetList();
        }

        public Order GetById(int id)
        {
            return _orderDal.Get(p => p.Id == id);
        }

        public void Update(Order Order)
        {
            _orderDal.Update(Order);
        }

        public void Add(Order Order)
        {
            _orderDal.Add(Order);
        }

        public void Delete(int id)
        {
            _orderDal.Delete(new Order { Id = id });
        }
    }
}