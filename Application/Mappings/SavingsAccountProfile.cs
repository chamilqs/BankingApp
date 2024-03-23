using AutoMapper;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Mappings
{
    public class SavingsAccountProfile : Profile
    {
        public SavingsAccountProfile()
        {

            #region SavingsAccountProfile

            CreateMap<SavingsAccount, SaveSavingsAccountViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore())
                .ForMember(c => c.Beneficiaries, opt => opt.Ignore());

            CreateMap<SavingsAccount, SavingsAccountViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore())
                .ForMember(c => c.Beneficiaries, opt => opt.Ignore());

            #endregion

        }
    }
}
