using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.ViewModels.Payment
{
    public class BeneficiaryPaymentViewModel
    {
        public string Destination { get; set; }
        public string Origin { get; set; }
        public double Amount { get; set; }
        public List<SavingsAccountViewModel>? LoggedUserAccounts { get; set; }

        public List<BeneficiaryViewModel>? LoggedUserBeneficiaries { get; set; }

    }
}
