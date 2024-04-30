using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.ViewModels.Payment
{
    public class LoanPaymentViewModel
    {
        public string Destination { get; set; }
        public string Origin { get; set; }
        public double Amount { get; set; }
        public List<LoanViewModel>? LoggedUserLoans { get; set; }

        public List<SavingsAccountViewModel>? LoggedUserAccounts { get; set; }

    }
}
