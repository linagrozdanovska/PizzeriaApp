using PizzeriaApp.Domain.DTO;
using System;

namespace PizzeriaApp.Services.Interface
{
    public interface ICartService
    {
        CartDto getCartInfo(string userId);
        bool deletePizzaFromCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
