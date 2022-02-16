using PizzeriaApp.Domain.Identity;
using System;
using System.Collections.Generic;

namespace PizzeriaApp.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Status { get; set; }
        public DateTime dateTime { get; set; }
        public string Address { get; set; }
        public virtual ICollection<PizzaInOrder> PizzasInOrder { get; set; }
    }
}
