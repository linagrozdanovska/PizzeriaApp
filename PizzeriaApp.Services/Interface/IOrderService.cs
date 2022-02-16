using PizzeriaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders(string userId);
        List<Order> GetAllOrders();
        Order GetOrderDetails(string userId, Guid id);
    }
}
