using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Application.ViewModels.Beneficiary
{
    public class SaveBeneficiaryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The client Id is required.")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "The account number is required.")]
        public string SavingsAccountId { get; set; } 

    }
}
