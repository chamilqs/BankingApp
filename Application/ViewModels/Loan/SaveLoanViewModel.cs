using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.ViewModels.Loan
{
    public class SaveLoanViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Amount Required.")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "ClientId Required.")]

        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }

        public double Balance { get; set; }
    }
}
