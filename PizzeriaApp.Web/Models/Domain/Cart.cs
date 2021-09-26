using PizzeriaApp.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Models.Domain
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public virtual ICollection<PizzaInCart> PizzasInCart { get; set; }
    }
}
