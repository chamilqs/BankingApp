using BankingApp.Core.Domain.Common;

namespace BankingApp.Core.Domain.Entities
{
    public class CreditCard : ProductBaseEntity
    {
        public double Limit { get; set; }
        public double Debt { get; set; }

    }
}
