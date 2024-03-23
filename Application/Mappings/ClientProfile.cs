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
                .ReverseMap()
                .ForMember(origin => origin.SavingsAccounts, opt => opt.Ignore())
                .ForMember(origin => origin.Beneficiaries, opt => opt.Ignore())
                .ForMember(origin => origin.CreditCards, opt => opt.Ignore())
                .ForMember(origin => origin.Loans, opt => opt.Ignore());

            CreateMap<Client, ClientViewModel>()
                .ReverseMap()
                .ForMember(origin => origin.SavingsAccounts, opt => opt.Ignore())
                .ForMember(origin => origin.Beneficiaries, opt => opt.Ignore())
                .ForMember(origin => origin.CreditCards, opt => opt.Ignore())
                .ForMember(origin => origin.Loans, opt => opt.Ignore());
            #endregion

        }
    }
}
