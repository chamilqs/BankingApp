using BankingApp.Core.Application.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ExpressPaymentViewModel> ExpressPayment(ExpressPaymentViewModel vm);
        Task<CreditCardPaymentViewModel> CreditCardPayment(CreditCardPaymentViewModel vm);
        Task<LoanPaymentViewModel> LoanPayment(LoanPaymentViewModel vm);
        Task<BeneficiaryPaymentViewModel> BeneficiaryPayment(BeneficiaryPaymentViewModel vm);
    }
}
