using AutoMapper;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Core.Application.Services
{
    public class ClientService : GenericService<SaveClientViewModel, ClientViewModel, Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserService userService) : base(clientRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientRepository = clientRepository;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        public async Task<ClientViewModel> GetByUserIdViewModel(string userId)
        {

            return new ClientViewModel();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm)
        {
            RegisterResponse response = await _userService.RegisterAsync(vm);

            if (!response.HasError)
            {
                //await _userService.fin(vm);
                //await _clientRepository.AddAsync();
            }

            return response;
        }
    }
}
