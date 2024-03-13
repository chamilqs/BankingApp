using BankingApp.Core.Domain.Common;

namespace BankingApp.Core.Domain.Entities
{
    public class Beneficiary : AuditableBaseEntity
    {
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public string SavingsAccountId { get; set; } // product 
        public SavingsAccount? SavingsAccount { get; set; }

    }
}
