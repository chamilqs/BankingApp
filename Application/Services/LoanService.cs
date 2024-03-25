using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanViewModel, LoanViewModel, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository loanRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<LoanViewModel>> GetAllByClientId(int clientId)
        {
            var loanList = await _loanRepository.GetAllAsync();
            return loanList.Where(l => l.ClientId == clientId).Select(l => new LoanViewModel
            {
                Id = l.Id,
                ClientId = l.ClientId,
                Amount = l.Amount,
                Balance = l.Balance,
                DateCreated = l.DateCreated



            }).ToList();
        }
    }
}
