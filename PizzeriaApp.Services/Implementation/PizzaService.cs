using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Domain.DTO;
using PizzeriaApp.Repository.Interface;
using PizzeriaApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaApp.Services.Implementation
{
    public class PizzaService : IPizzaService
    {
        private readonly IRepository<Pizza> _PizzaRepository;
        private readonly IRepository<PizzaInCart> _PizzaInCartRepository;
        private readonly IUserRepository _userRepository;

        public PizzaService(IRepository<Pizza> PizzaRepository, IUserRepository userRepository, IRepository<PizzaInCart> PizzaInCartRepository)
        {
            _PizzaRepository = PizzaRepository;
            _userRepository = userRepository;
            _PizzaInCartRepository = PizzaInCartRepository;
        }

        public List<Pizza> GetAllPizzas()
        {
            return this._PizzaRepository.GetAll().ToList();
        }

        public Pizza GetDetailsForPizza(Guid? id)
        {
            return this._PizzaRepository.Get(id);
        }

        public void CreateNewPizza(Pizza t)
        {
            this._PizzaRepository.Insert(t);
        }

        public void UpdeteExistingPizza(Pizza t)
        {
            this._PizzaRepository.Update(t);
        }

        public AddPizzaToCartDto GetCartInfo(Guid? id)
        {
            var Pizza = this.GetDetailsForPizza(id);
            AddPizzaToCartDto model = new AddPizzaToCartDto
            {
                SelectedPizza = Pizza,
                PizzaId = Pizza.Id,
                Quantity = 1
            };
            return model;
        }

        public void DeletePizza(Guid id)
        {
            var Pizza = this.GetDetailsForPizza(id);
            this._PizzaRepository.Delete(Pizza);
        }

        public bool AddToCart(AddPizzaToCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userCart = user.UserCart;
            if (item.PizzaId != null && userCart != null)
            {
                var Pizza = this.GetDetailsForPizza(item.PizzaId);
                if (Pizza != null)
                {
                    PizzaInCart itemToAdd = new PizzaInCart
                    {
                        Id = Guid.NewGuid(),
                        Pizza = Pizza,
                        PizzaId = Pizza.Id,
                        Cart = userCart,
                        CartId = userCart.Id,
                        Quantity = item.Quantity
                    };
                    this._PizzaInCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
