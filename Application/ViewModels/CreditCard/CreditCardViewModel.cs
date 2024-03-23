namespace BankingApp.Core.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Balance { get; set; }
        public double Limit { get; set; }
        public double Debt { get; set; }
    }
}
