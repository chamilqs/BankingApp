using BankingApp.Core.Application.Interfaces.Services;

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
            //if (creditCardList.Any(cc => cc.Id == productNumber))
            //{
            //    return true;
            //}
            //if (loanList.Any(loan => loan.Id == productNumber))
            //{
            //    return true;
            //}

            return false;
        }
    }
}
