using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Services.Interface;
using System;
using System.Security.Claims;

namespace PizzeriaApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._orderService.GetAllOrders(userId));
        }

        public IActionResult Details(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._orderService.GetOrderDetails(userId, id));
        }
    }
}
