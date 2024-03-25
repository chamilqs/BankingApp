using BankingApp.Core.Application.ViewModels.Products;
using BankingApp.Core.Application.ViewModels.SavingsAccount;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<string> GenerateProductNumber();
        Task<ProductViewModel> GetAllProductsByClient(int clientId);
    }
}
