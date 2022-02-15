using System;

namespace PizzeriaApp.Domain.DomainModels
{
    public class PizzaInCart : BaseEntity
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public string PizzaSize { get; set; }
        public int PizzaPrice { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}
