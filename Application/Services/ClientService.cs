using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class ClientService : GenericService<SaveClientViewModel, ClientViewModel, Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserService _userService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper, IUserService userService, 
            ISavingsAccountService savingsAccountService, 
            IProductService productService) : base(clientRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientRepository = clientRepository;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
            _savingsAccountService = savingsAccountService;
            _productService = productService;
        }

        public async Task<ClientViewModel> GetByUserIdViewModel(string userId)
        {
            var clientList = await base.GetAllViewModel();

            ClientViewModel client = clientList.FirstOrDefault(client => client.UserId == userId);
                
            return new ClientViewModel();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm)
        {
            RegisterResponse response = await _userService.RegisterAsync(vm);

            if (!response.HasError)
            {
                var user = await _userService.GetByUsername(vm.Username);

                SaveClientViewModel saveClientViewModel = new() 
                { 
                    UserId = user.Id,
                    DateCreated = DateTime.UtcNow,
                };

                var client = await base.Add(saveClientViewModel);

                SaveSavingsAccountViewModel savingsAccountViewModel = new()
                {
                    Id = await _productService.GenerateProductNumber(),
                    ClientId = client.Id,
                    Balance = vm.AccountAmount.Value,
                    DateCreated = DateTime.UtcNow,
                    IsMainAccount = true
                };

                await _savingsAccountService.Add(savingsAccountViewModel);
            }

            return response;
        }
    }
}
