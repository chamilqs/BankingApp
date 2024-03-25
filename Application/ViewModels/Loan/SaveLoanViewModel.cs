using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Application.ViewModels.Loan
{
    public class SaveLoanViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "You must enter an amount.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "You must specify a client.")]
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public double? Balance { get; set; }
    }
}
