using PizzeriaApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Models.DTO
{
    public class CartDto
    {
        public List<PizzaInCart> PizzasInCart { get; set; }
        public int TotalPrice { get; set; }
    }
}
