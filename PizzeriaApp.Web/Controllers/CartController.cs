using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaApp.Web.Data;
using PizzeriaApp.Web.Models.DTO;
using PizzeriaApp.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzeriaApp.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users
                .Where(z => z.Id == userId)
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.PizzasInCart)
                .Include("UserCart.PizzasInCart.Pizza")
                .FirstOrDefaultAsync();
            var userCart = loggedInUser.UserCart;
            var pizzaPrice = userCart.PizzasInCart.Select(z => new { 
                PizzaPrice = z.Pizza.PizzaPrice,
                Quantity = z.Quantity
            }).ToList();
            var totalPrice = 0;
            foreach (var item in pizzaPrice)
            {
                totalPrice += item.PizzaPrice * item.Quantity;
            }
            CartDto cartDtoItem = new CartDto
            {
                PizzasInCart = userCart.PizzasInCart.ToList(),
                TotalPrice = totalPrice
            };
            //var allPRoducts = userCart.PizzasInCart.Select(z => z.Pizza).ToList();
            return View(cartDtoItem);
        }
    }
}
