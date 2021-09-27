using PizzeriaApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Models.DTO
{
    public class AddPizzaToCartDto
    {
        public Pizza SelectedPizza { get; set; }
        public Guid PizzaId { get; set; }
        public int Quantity { get; set; }
    }
}
