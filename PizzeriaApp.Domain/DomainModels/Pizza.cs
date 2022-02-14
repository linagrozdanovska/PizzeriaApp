using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaApp.Domain.DomainModels
{
    public class Pizza : BaseEntity
    {
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
        public virtual ICollection<PizzaInOrder> PizzasInOrder { get; set; }
    }
}
