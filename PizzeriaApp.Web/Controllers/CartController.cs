using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Services.Interface;
using Stripe;
using System;
using System.Security.Claims;

namespace PizzeriaApp.Web.Controllers
{
    [Authorize(Roles = "StandardUser,Admin,Delivery")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._cartService.getCartInfo(userId));
        }

        public IActionResult DeletePizzaFromCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cartService.deletePizzaFromCart(userId, id);
            if (result)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        private Boolean OrderNow(string deliveryAddress)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cartService.orderNow(userId, deliveryAddress);
            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken, string deliveryAddress)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._cartService.getCartInfo(userId);
            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = order.TotalPrice * 100,
                Description = "PizzeriaApp Payment",
                Currency = "usd",
                Customer = customer.Id
            });
            if (charge.Status == "succeeded")
            {
                var result = this.OrderNow(deliveryAddress);
                if (result)
                {
                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
