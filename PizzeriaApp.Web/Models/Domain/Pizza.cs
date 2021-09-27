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
        [Display(Name = "Pizza")]
        public string PizzaName { get; set; }
        [Required]
        [Display(Name = "Ingredients")]
        public string PizzaIngredients { get; set; }
        [Required]
        [Display(Name = "Price")]
        public int PizzaPrice { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string PizzaImage { get; set; }
        public virtual ICollection<PizzaInCart> PizzasInCart { get; set; }
    }
}
