﻿using PizzeriaApp.Domain.DomainModels;
using System;

namespace PizzeriaApp.Domain.DTO
{
    public class AddPizzaToCartDto
    {
        public Pizza SelectedPizza { get; set; }
        public Guid PizzaId { get; set; }
        public string PizzaSize { get; set; }
        public int PizzaPrice { get; set; }
        public int Quantity { get; set; }
    }
}
