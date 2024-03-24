using AutoMapper;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Mappings
{
    public class BeneficiaryProfile : Profile
    {
        public BeneficiaryProfile()
        {
            #region BeneficiaryProfile

            CreateMap<Beneficiary, SaveBeneficiaryViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(b => b.Client, opt => opt.Ignore())
                .ForMember(b => b.SavingsAccount, opt => opt.Ignore());

            CreateMap<Beneficiary, BeneficiaryViewModel>()
                .ForMember(x => x.BeneficiaryName, opt => opt.Ignore())
                .ForMember(x => x.BeneficiaryLastName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(b => b.Client, opt => opt.Ignore())
                .ForMember(b => b.SavingsAccount, opt => opt.Ignore());

            #endregion

        }
    }
}
