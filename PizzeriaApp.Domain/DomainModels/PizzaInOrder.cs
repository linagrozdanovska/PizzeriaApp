using System;

namespace PizzeriaApp.Domain.DomainModels
{
    public class PizzaInOrder : BaseEntity
    {
        public Guid PizzaId { get; set; }
        public Pizza SelectedPizza { get; set; }
        public string PizzaSize { get; set; }
        public int PizzaPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
