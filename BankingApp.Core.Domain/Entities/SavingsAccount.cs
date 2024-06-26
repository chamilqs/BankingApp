﻿using BankingApp.Core.Domain.Common;

namespace BankingApp.Core.Domain.Entities
{
    public class SavingsAccount : ProductBaseEntity
    {
        public bool IsMainAccount { get; set; }
        public ICollection<Beneficiary>? Beneficiaries { get; set; }
    }
}
