using BankingApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using BankingApp.Core.Application.Interfaces.Services;
using System.Reflection;

namespace BankingApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISavingsAccountService, SavingsAccountService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITransfersService, TransfersService>();
            #endregion
        }
    }
}
