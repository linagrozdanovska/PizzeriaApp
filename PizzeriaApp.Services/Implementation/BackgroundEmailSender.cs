using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Repository.Interface;
using PizzeriaApp.Services.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaApp.Services.Implementation
{
    public class BackgroundEmailSender : IBackgroundEmailSender
    {
        private readonly IEmailService _emailService;
        private readonly IRepository<EmailMessage> _mailRepository;

        public BackgroundEmailSender(IEmailService emailService, IRepository<EmailMessage> mailRepository)
        {
            _emailService = emailService;
            _mailRepository = mailRepository;
        }

        public async Task DoWork()
        {
            await _emailService.SendEmailAsync(_mailRepository.GetAll().Where(z => !z.Status).ToList());
            foreach (var item in _mailRepository.GetAll().Where(z => !z.Status).ToList())
            {
                item.Status = true;
                this._mailRepository.Update(item);
            }
        }
    }
}
