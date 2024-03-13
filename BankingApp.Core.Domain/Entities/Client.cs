namespace BankingApp.Core.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<SavingsAccount>? SavingsAccounts { get; set; }
        public ICollection<Beneficiary>? Beneficiaries { get; set;}
        public ICollection<CreditCard>? CreditCards { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}


