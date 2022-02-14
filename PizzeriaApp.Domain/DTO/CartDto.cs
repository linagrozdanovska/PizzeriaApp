using PizzeriaApp.Domain.DomainModels;
using System.Collections.Generic;

namespace PizzeriaApp.Domain.DTO
{
    public class CartDto
    {
        public List<PizzaInCart> PizzasInCart { get; set; }
        public int TotalPrice { get; set; }
    }
}
