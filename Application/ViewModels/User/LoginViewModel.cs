using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You mus enter an email.")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
