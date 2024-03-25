using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.SavingsAccount;

namespace BankingApp.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int ClientId { get; set; }
        public List<SavingsAccountViewModel> SavingsAccounts { get; set; }
        public List<LoanViewModel> Loans { get; set; }
        public List<CreditCardViewModel> CreditCards { get; set; }
    }
}
