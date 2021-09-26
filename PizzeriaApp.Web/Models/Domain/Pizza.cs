using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Models.Domain
{
    public class Pizza
    {
        public Guid Id { get; set; }
        [Required]
        public string PizzaName { get; set; }
        [Required]
        public string PizzaIngredients { get; set; }
        [Required]
        public int PizzaPrice { get; set; }
        [Required]
        public string PizzaImage { get; set; }
        public virtual ICollection<PizzaInCart> PizzasInCart { get; set; }
    }
}
