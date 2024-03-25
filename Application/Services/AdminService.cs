using AutoMapper;
using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.User;

namespace BankingApp.Core.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAccountService _accountService;
        // private readonly PaymentService _paymentService;
        private readonly ITransactionService _transactionService;
        private readonly ILoanService _loanService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMapper _mapper;

        public AdminService(IAccountService accountService, ITransactionService transactionService, ILoanService loanService, ISavingsAccountService savingsAccountService, ICreditCardService creditCardService, IMapper mapper)
        {
            _accountService = accountService;
            // _paymentService = paymentService;
            _transactionService = transactionService;
            _loanService = loanService;
            _savingsAccountService = savingsAccountService;
            _creditCardService = creditCardService;
            _mapper = mapper;
        }

        public async Task<int> GetActiveUsersCount()
        {
			return await _accountService.GetActiveUsersCountAsync();
		}

        public async Task<int> GetInactiveUsersCount()
        {
			return await _accountService.GetInactiveUsersCountAsync();
		}

        public async Task<int> GetTotalPaymentsCount()
        {
            return 0;
			// var payments = await _paymentsService.GetAllViewModel();
            // return payments.Count;
		}

		public async Task<int> GetTodayTotalPaymentsCount()
        {
            return 0;
			// var payments = await _paymentsService.GetAllViewModel();
			// payments = payments.Where(x => x.DateCreated.Day == DateTime.Now.Day).ToList();

            // return payments.Count;
		}

		public async Task<int> GetTotalTransactionsCount()
        {
            var transactions = await _transactionService.GetAllViewModel();
            if(transactions == null)
            {
				return 0;
			}           
            
            return transactions.Count;
		}

        public async Task<int> GetTodayTotalTransactionsCount()
        {
			var transactions = await _transactionService.GetAllViewModel();
            transactions = transactions.Where(x => x.DateCreated.Day == DateTime.Now.Day).ToList();
            
            if(transactions == null)
            {
                return 0;
            }

			return transactions.Count;
		}

        public async Task<int> GetTotalLoansCount()
        {
            var loans = await _loanService.GetAllViewModel();
            if(loans == null)
            {
				return 0;
			}

            return loans.Count;
        }

        public async Task<int> GetTotalSavingsAccountsCount()
        {
            var savingsAccounts = await _savingsAccountService.GetAllViewModel();
            if(savingsAccounts == null)
            {
                return 0;
            }


            return savingsAccounts.Count;
		}

        public async Task<int> GetTotalCreditCardsCount()
        {
            var creditCards = await _creditCardService.GetAllViewModel();
            if(creditCards == null)
            {
				return 0;
			}
            return creditCards.Count;
		}
	


        #region GetAllViewModel
        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _accountService.GetAllUserAsync();

            return _mapper.Map<List<UserViewModel>>(userList);
        }
        #endregion

        #region UpdateUserStatus
        public async Task<GenericResponse> UpdateUserStatus(string userId)
        {
            var userStatus = await _accountService.UpdateUserStatusAsync(userId);

            return userStatus;
        }
        #endregion
    }
}
