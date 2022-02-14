using Microsoft.AspNetCore.Identity;
using PizzeriaApp.Domain.DomainModels;

namespace PizzeriaApp.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual Cart UserCart { get; set; }
    }
}
