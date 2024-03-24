namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<string> GenerateProductNumber();
    }
}
