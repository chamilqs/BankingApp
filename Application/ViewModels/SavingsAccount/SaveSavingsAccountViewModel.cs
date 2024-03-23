namespace BankingApp.Core.Application.ViewModels.SavingsAccount
{
    public class SaveSavingsAccountViewModel
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Balance { get; set; }
        public bool isMainAccount { get; set; }
    }
}
