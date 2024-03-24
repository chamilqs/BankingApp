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
        Task<PaymentViewModel> ExpressPayment(PaymentViewModel vm);
        Task<PaymentViewModel> CreditCardPayment(PaymentViewModel vm);
        Task<PaymentViewModel> LoanPayment(PaymentViewModel vm);
        Task<PaymentViewModel> BeneficiaryPayment(PaymentViewModel vm);
    }
}
