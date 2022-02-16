using Microsoft.EntityFrameworkCore;
using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders(string userId)
        {
            return entities
                .Where(z => z.UserId.Equals(userId))
                .Include(z => z.PizzasInOrder)
                .Include("PizzasInOrder.SelectedPizza")
                .Include(z => z.User)
                .ToListAsync().Result;
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.PizzasInOrder)
                .Include("PizzasInOrder.SelectedPizza")
                .Include(z => z.User)
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(Guid id)
        {
            return entities
                .Where(z => z.Id == id)
                .Include(z => z.PizzasInOrder)
                .Include("PizzasInOrder.SelectedPizza")
                .Include(z => z.User)
                .SingleOrDefaultAsync().Result;
        }

        public void UpdateOrderStatus(Guid id, string status)
        {
            Order order = entities
                .Where(z => z.Id == id)
                .SingleOrDefault();
            order.Status = status;
            context.SaveChanges();
        }
    }
}
