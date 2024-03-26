using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Application.ViewModels.CreditCard
{
    public class SaveCreditCardViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "You must specify a client.")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "You must enter a limit.")]
        public double Limit { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public double? Balance { get; set; }
        public double? Debt { get; set; }
    }
}
