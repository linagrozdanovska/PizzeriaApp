using PizzeriaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders(string UserId);
        List<Order> GetAllOrders();
        Order GetOrderDetails(string userId, Guid id);
    }
}
