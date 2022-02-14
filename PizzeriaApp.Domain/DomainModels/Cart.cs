using PizzeriaApp.Domain.Identity;
using System.Collections.Generic;

namespace PizzeriaApp.Domain.DomainModels
{
    public class Cart : BaseEntity
    {
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public virtual ICollection<PizzaInCart> PizzasInCart { get; set; }
    }
}
