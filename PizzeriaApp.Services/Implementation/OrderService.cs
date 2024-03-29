﻿using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Repository.Interface;
using PizzeriaApp.Services.Interface;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public List<Order> GetAllOrders(string userId)
        {
            return this._orderRepository.GetAllOrders(userId);
        }

        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order GetOrderDetails(Guid id)
        {
            return this._orderRepository.GetOrderDetails(id);
        }
        public void UpdeteOrderStatus(Guid id, string status)
        {
            this._orderRepository.UpdateOrderStatus(id, status);
        }
    }
}
