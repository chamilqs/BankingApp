using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Domain.Settings;
using BankingApp.Infrastructure.Shared.Services;

namespace BankingApp.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));         
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
