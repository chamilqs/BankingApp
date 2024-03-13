using BankingApp.Core.Domain.Common;

namespace BankingApp.Core.Domain.Entities
{
    public class Loan : ProductBaseEntity
    {
        public double Amount { get; set; }

    }
}
