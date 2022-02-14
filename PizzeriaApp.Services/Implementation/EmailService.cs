using MailKit.Security;
using MimeKit;
using PizzeriaApp.Domain;
using PizzeriaApp.Domain.DomainModels;
using PizzeriaApp.Services.Interface;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PizzeriaApp.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();
            foreach (var item in allMails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_emailSettings.SenderName, _emailSettings.SmtpUsername),
                    Subject = item.Subject
                };
                emailMessage.From.Add(new MailboxAddress(_emailSettings.EmailDisplayName, _emailSettings.SmtpUsername));
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = item.Content };
                emailMessage.To.Add(new MailboxAddress(item.MailTo));
                messages.Add(emailMessage);
            }
            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, socketOption);
                    if (!string.IsNullOrEmpty(_emailSettings.SmtpUsername))
                    {
                        await smtp.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                    }
                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException exception)
            {
                throw exception;
            }
        }
    }
}
