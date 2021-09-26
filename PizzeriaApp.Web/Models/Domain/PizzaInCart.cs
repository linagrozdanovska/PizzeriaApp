using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Models.Domain
{
    public class PizzaInCart
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}
