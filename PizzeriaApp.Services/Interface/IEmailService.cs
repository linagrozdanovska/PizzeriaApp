using PizzeriaApp.Domain.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzeriaApp.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
