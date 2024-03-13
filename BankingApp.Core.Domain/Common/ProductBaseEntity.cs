using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Domain.Common
{
    public class ProductBaseEntity
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime DateCreated { get; set; }
        public double Balance { get; set; }

    }
}
