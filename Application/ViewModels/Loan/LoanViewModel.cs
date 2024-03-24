using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.ViewModels.Loan
{
    public class LoanViewModel
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Balance { get; set; }
        public double Amount { get; set; }


    }
}
