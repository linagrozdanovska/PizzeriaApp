﻿using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Domain.DTO;
using PizzeriaApp.Repository.Interface;
using PizzeriaApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaApp.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<PizzaInOrder> _PizzaInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<EmailMessage> _mailRepository;

        public CartService(IRepository<Cart> cartRepository, IRepository<PizzaInOrder> PizzaInOrderRepository, IRepository<Order> orderRepository, IUserRepository userRepository, IRepository<EmailMessage> mailRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _PizzaInOrderRepository = PizzaInOrderRepository;
            _mailRepository = mailRepository;
        }

        public bool deletePizzaFromCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;
                var itemToDelete = userCart.PizzasInCart.Where(z => z.PizzaId.Equals(id)).FirstOrDefault();
                userCart.PizzasInCart.Remove(itemToDelete);
                this._cartRepository.Update(userCart);
                return true;
            }
            return false;
        }

        public CartDto getCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);
            var userCart = loggedInUser.UserCart;
            var allPizzas = userCart.PizzasInCart.ToList();

            var PizzaPrices = allPizzas.Select(z => new
            {
                PizzaPrice = z.PizzaPrice,
                Quantity = z.Quantity
            }).ToList();
            var totalPrice = 0;
            foreach (var item in PizzaPrices)
            {
                totalPrice += item.PizzaPrice * item.Quantity;
            }
            CartDto cartDto = new CartDto
            {
                PizzasInCart = allPizzas,
                TotalPrice = totalPrice
            };
            return cartDto;
        }

        public bool orderNow(string userId, string deliveryAddress)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;
                EmailMessage emailMessage = new EmailMessage();
                emailMessage.MailTo = loggedInUser.Email;
                emailMessage.Subject = "Successfully created order";
                emailMessage.Status = false;
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser,
                    Status = "Order Placed",
                    Address = deliveryAddress,
                    DateTime = DateTime.Now
                };
                this._orderRepository.Insert(order);
                List<PizzaInOrder> PizzasInOrder = new List<PizzaInOrder>();
                var result = userCart.PizzasInCart.Select(z => new PizzaInOrder
                {
                    Id = Guid.NewGuid(),
                    PizzaId = z.Pizza.Id,
                    SelectedPizza = z.Pizza,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity,
                    PizzaPrice = z.PizzaPrice,
                    PizzaSize = z.PizzaSize
                }).ToList();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Your order has been placed.");
                int totalQuantity = 0;
                int totalPrice = 0;
                for (int i = 1; i <= result.Count; i++)
                {
                    var item = result[i - 1];
                    totalQuantity += item.Quantity;
                    totalPrice += item.Quantity * item.PizzaPrice;
                    stringBuilder.AppendLine("\n" + i.ToString() + ". " + item.Quantity + " x " + item.SelectedPizza.PizzaName + "(" + item.PizzaSize + ")" + ", Ingredients: " + item.SelectedPizza.PizzaIngredients + ", Price: $" + item.PizzaPrice);
                }
                stringBuilder.AppendLine("\nTotal Quantity: " + totalQuantity.ToString());
                stringBuilder.AppendLine("Total Price: $" + totalPrice.ToString());
                emailMessage.Content = stringBuilder.ToString();
                PizzasInOrder.AddRange(result);
                foreach (var item in PizzasInOrder)
                {
                    this._PizzaInOrderRepository.Insert(item);
                }
                loggedInUser.UserCart.PizzasInCart.Clear();
                this._mailRepository.Insert(emailMessage);
                this._userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
