using PizzeriaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders(string UserId);
        List<Order> GetAllOrders();
        Order GetOrderDetails(Guid id);
        void UpdateOrderStatus(Guid id, string status);
    }
}
