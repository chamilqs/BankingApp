using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.Products;
using BankingApp.Core.Application.ViewModels.SavingsAccount;

namespace BankingApp.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;

        public ProductService(ILoanService loanService, ICreditCardService creditCardService, ISavingsAccountService savingsAccountService)
        {
            _savingsAccountService = savingsAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;
        }

        public async Task<string> GenerateProductNumber()
        {
            string accNumber = "000000000";

            while (true)
            {
                Random randomNumber = new Random();
                int nineDigitNumber = randomNumber.Next(1, 1000000000);
                accNumber = nineDigitNumber.ToString("000000000");

                if (!await ExistsProduct(accNumber))
                {
                    break;
                }
            }

            return accNumber; // Example output  627993046
        }

        private async Task<bool> ExistsProduct(string productNumber)
        {
            var savingsAccountList = await _savingsAccountService.GetAllViewModel();
            var creditCardList = await _creditCardService.GetAllViewModel();
            var loanList = await _loanService.GetAllViewModel();

            if (savingsAccountList.Any(sa => sa.Id == productNumber))
            {
                return true;
            }
            if (creditCardList.Any(cc => cc.Id == productNumber))
            {
                return true;
            }
            if (loanList.Any(loan => loan.Id == productNumber))
            {
                return true;
            }

            return false;
        }

        #region GetAllProductsByClient
        public async Task<ProductViewModel> GetAllProductsByClient(int clientId)
        {
            List<SavingsAccountViewModel> savingsAccountList = await GetAllSavingsAccounts(clientId);
            List<CreditCardViewModel> creditCardList = await GetAllCreditCards(clientId);
            List<LoanViewModel> loanList = await GetAllLoans(clientId);

            ProductViewModel productViewModel = new()
            {
                ClientId = clientId,
                SavingsAccounts = savingsAccountList,
                CreditCards = creditCardList,
                Loans = loanList,
            };

            return productViewModel;
        }

        private async Task<List<SavingsAccountViewModel>> GetAllSavingsAccounts(int clientId)
        {
            var savingsAccountList = await _savingsAccountService.GetAllViewModel();
            savingsAccountList = savingsAccountList.FindAll(cc => cc.ClientId == clientId);

            return savingsAccountList;
        }

        private async Task<List<CreditCardViewModel>> GetAllCreditCards(int clientId)
        {
            var creditCardList = await _creditCardService.GetAllViewModel();
            creditCardList = creditCardList.FindAll(cc => cc.ClientId == clientId);

            return creditCardList;
        }

        private async Task<List<LoanViewModel>> GetAllLoans(int clientId)
        {
            var loanList = await _loanService.GetAllViewModel();
            loanList = loanList.FindAll(cc => cc.ClientId == clientId);

            return loanList;
        }
        #endregion
    }
}
