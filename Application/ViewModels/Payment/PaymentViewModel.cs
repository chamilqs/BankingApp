using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.ViewModels.Payment
{
    public class PaymentViewModel
    {
        public string Destination { get; set; }
        public string Origin { get; set; }
        public double Amount { get; set; }


    }
}
