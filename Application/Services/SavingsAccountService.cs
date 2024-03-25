using AutoMapper;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Services
{
    public class SavingsAccountService : GenericService<SaveSavingsAccountViewModel, SavingsAccountViewModel, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly IMapper _mapper;

        public SavingsAccountService(ISavingsAccountRepository savingsAccountRepository, IMapper mapper) : base(savingsAccountRepository, mapper)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _mapper = mapper;
        }

        public async Task<SavingsAccount> GetByAccountNumber(string accountNumber)
        {
            var savingsAccount = await _savingsAccountRepository.GetByAccountNumber(accountNumber);
            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;
        }

        public async Task<SavingsAccount> GetByAccountNumberLoggedUser(string accountNumber, int ClientId)
        {
            var savingsAccount = await _savingsAccountRepository.GetByAccountNumberLoggedUser(accountNumber, ClientId);
            if (savingsAccount == null)
            {
                return null;
            }

            return savingsAccount;
        }

        public async Task UpdateSavingsAccount(double balance, int clientId, string id)
        {
            var savingsAccount = await GetByAccountNumberLoggedUser(id, clientId);

            SaveSavingsAccountViewModel vm = new SaveSavingsAccountViewModel
            {
                Id = savingsAccount.Id,
                ClientId = clientId,
                DateCreated = savingsAccount.DateCreated,
                Balance = balance,
                IsMainAccount = savingsAccount.IsMainAccount
            };

            await base.UpdateProduct(vm, id);
        }

        #region GetClientMainAccount
        public async Task<SavingsAccountViewModel> GetClientMainAccount(int clientId)
        {
            var savingsAccountList = await base.GetAllViewModel();

            var savingsAccount = savingsAccountList.FirstOrDefault(sa => sa.ClientId == clientId && sa.IsMainAccount == true);

            return savingsAccount;
        }
        #endregion
    }
}
