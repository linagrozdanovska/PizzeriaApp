using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Domain.DTO;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Services.Interface
{
    public interface IPizzaService
    {
        List<Pizza> GetAllPizzas();
        Pizza GetDetailsForPizza(Guid? id);
        void CreateNewPizza(Pizza p);
        void UpdeteExistingPizza(Pizza p);
        AddPizzaToCartDto GetCartInfo(Guid? id);
        void DeletePizza(Guid id);
        bool AddToCart(AddPizzaToCartDto item, string userID);
    }
}
}
