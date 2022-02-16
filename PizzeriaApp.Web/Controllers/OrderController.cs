using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Services.Interface;
using System;
using System.Security.Claims;

namespace PizzeriaApp.Web.Controllers
{
    [Authorize(Roles = "StandardUser,Admin,Delivery")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Delivery") || User.IsInRole("Admin"))
            {
                return View(this._orderService.GetAllOrders());
            }
            else
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(this._orderService.GetAllOrders(userId));
            }
        }

        public IActionResult Details(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._orderService.GetOrderDetails(userId, id));
        }
    }
}
