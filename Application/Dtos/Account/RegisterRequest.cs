﻿namespace BankingApp.Core.Application.DTOs.Account
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string? Role { get; set; }
        public string IdentificationUser { get; set; }
        public bool IsActive { get; set; }
    }
}
