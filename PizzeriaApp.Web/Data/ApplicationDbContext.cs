using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzeriaApp.Web.Models.Domain;
using PizzeriaApp.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<PizzaInCart> PizzasInCart { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pizza>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Cart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<PizzaInCart>()
                .HasKey(z => new { z.PizzaId, z.CartId });

            builder.Entity<PizzaInCart>()
                .HasOne(z => z.Pizza)
                .WithMany(z => z.PizzasInCart)
                .HasForeignKey(z => z.CartId);

            builder.Entity<PizzaInCart>()
                .HasOne(z => z.Cart)
                .WithMany(z => z.PizzasInCart)
                .HasForeignKey(z => z.PizzaId);

            builder.Entity<Cart>()
                .HasOne<AppUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<Cart>(z => z.OwnerId);
        }
    }
}
