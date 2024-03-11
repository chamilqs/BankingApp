using BankingApp.Core.Application.DTOs.Email;
using BankingApp.Core.Domain.Settings;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
