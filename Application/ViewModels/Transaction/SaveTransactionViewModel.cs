﻿using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Application.ViewModels.Transaction
{
    public class SaveTransactionViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "The origin account is required")]
        public string Origin { get; set; }
        [Required (ErrorMessage = "The destination account is required")]
        public string Destination { get; set; }
        public int? TransactionTypeId { get; set; }
        [Required (ErrorMessage = "The amount is required")]
        public double Amount { get; set; }
        public string? Concept { get; set; }
        public DateTime DateCreated { get; set; } 

    }
}
