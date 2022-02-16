using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Services.Interface;
using System;
using System.IO;
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
            return View(this._orderService.GetOrderDetails(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delivery")]
        public IActionResult ChangeStatus(Guid id, string Status)
        {
            if (ModelState.IsValid)
            {
                this._orderService.UpdeteOrderStatus(id, Status);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportOrders()
        {
            var allOrders = this._orderService.GetAllOrders();
            string result = "Displaying all orders.";
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");
                worksheet.Cell(1, 1).Value = result;
                worksheet.Cell(3, 1).Value = "Date & Time";
                worksheet.Cell(3, 2).Value = "Status";
                worksheet.Cell(3, 3).Value = "User";
                worksheet.Cell(3, 4).Value = "Address";
                worksheet.Cell(3, 5).Value = "Ordered pizzas";
                worksheet.Cell(3, 6).Value = "Total";
                for (int i = 0; i < allOrders.Count; i++)
                {
                    var item = allOrders[i];
                    worksheet.Cell(i + 4, 1).Value = item.DateTime;
                    worksheet.Cell(i + 4, 2).Value = item.Status;
                    worksheet.Cell(i + 4, 3).Value = item.User;
                    worksheet.Cell(i + 4, 4).Value = item.Address;
                    var str = "";
                    foreach (var p in item.PizzasInOrder)
                    {
                        str = p.Quantity + " x " + p.SelectedPizza.PizzaName + "(" + p.PizzaSize + ")\n";
                    }
                    worksheet.Cell(i + 4, 5).Value = str;
                    var total = 0;
                    foreach (var p in item.PizzasInOrder)
                    {
                        total += p.Quantity * p.PizzaPrice;
                    }
                    worksheet.Cell(i + 4, 6).Value = total;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
