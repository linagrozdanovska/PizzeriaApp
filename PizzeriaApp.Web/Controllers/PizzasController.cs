﻿using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Domain.DTO;
using PizzeriaApp.Services.Interface;

namespace PizzeriaApp.Web.Controllers
{
    public class PizzasController : Controller
    {
        private readonly IPizzaService _pizzaService;

        public PizzasController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        // GET: Pizzas
        public IActionResult Index()
        {
            var allpizzas = this._pizzaService.GetAllPizzas();
            return View(allpizzas);
        }

        // GET: Pizzas/AddPizzaToCart/5
        public IActionResult AddPizzaToCart(Guid? id)
        {
            var model = this._pizzaService.GetCartInfo(id);
            return View(model);
        }

        [Authorize(Roles = "StandardUser,Admin,Delivery")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPizzaToCart([Bind("SelectedPizza, PizzaId, Quantity, PizzaSize, PizzaPrice")]AddPizzaToCartDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var scale = 0;
            if (item.PizzaSize.Equals("Small"))
            {
                scale = -2;
            }
            else if (item.PizzaSize.Equals("Large"))
            {
                scale = 2;
            }
            item.PizzaPrice += scale;
            var result = this._pizzaService.AddToCart(item, userId);
            if (result)
            {
                return RedirectToAction("Index", "Cart");
            }
            return View(item);
        }

        // GET: Pizzas/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = this._pizzaService.GetDetailsForPizza(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,PizzaName,PizzaIngredients,PizzaPrice,PizzaImage")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                this._pizzaService.CreateNewPizza(pizza);
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = this._pizzaService.GetDetailsForPizza(id);

            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Id,PizzaName,PizzaIngredients,PizzaPrice,PizzaImage")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._pizzaService.UpdeteExistingPizza(pizza);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = this._pizzaService.GetDetailsForPizza(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._pizzaService.DeletePizza(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(Guid id)
        {
            return this._pizzaService.GetDetailsForPizza(id) != null;
        }
    }
}
