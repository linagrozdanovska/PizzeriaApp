using Microsoft.EntityFrameworkCore;
using PizzeriaApp.Domain.Identity;
using PizzeriaApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<AppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<AppUser>();
        }
        public IEnumerable<AppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public AppUser Get(string id)
        {
            return entities
                .Include(z => z.UserCart)
                .Include("UserCart.PizzasInCart")
                .Include("UserCart.PizzasInCart.Pizza")
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
