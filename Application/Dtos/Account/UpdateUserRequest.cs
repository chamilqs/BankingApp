﻿namespace BankingApp.Core.Application.Dtos.Account
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string IdentificationNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
