using PizzeriaApp.Domain.Identity;
using System.Collections.Generic;

namespace PizzeriaApp.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<AppUser> GetAll();
        AppUser Get(string id);
        void Insert(AppUser entity);
        void Update(AppUser entity);
        void Delete(AppUser entity);
    }
}