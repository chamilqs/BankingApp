using AutoMapper;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            #region ClientProfile

            CreateMap<Client, SaveBeneficiaryViewModel>()
                .ReverseMap();

            CreateMap<Client, ClientViewModel>()

                .ReverseMap();

            #endregion

        }
    }
}
