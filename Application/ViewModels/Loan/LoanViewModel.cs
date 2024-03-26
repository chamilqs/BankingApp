namespace BankingApp.Core.Application.ViewModels.Loan
{
    public class LoanViewModel
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
    }
}
