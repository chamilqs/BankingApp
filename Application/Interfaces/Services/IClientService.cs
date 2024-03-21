using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IClientService : IGenericService<SaveClientViewModel, ClientViewModel, Client>
    {
        Task<ClientViewModel> GetByUserIdViewModel(string userId);
    }
}
