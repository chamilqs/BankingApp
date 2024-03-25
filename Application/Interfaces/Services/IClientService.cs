using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IClientService : IGenericService<SaveClientViewModel, ClientViewModel, Client>
    {
        Task<ClientViewModel> GetByUserIdViewModel(string userId);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm);
        Task<GenericResponse> UpdateAsync(SaveUserViewModel vm);
    }
}
