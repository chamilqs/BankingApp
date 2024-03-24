using AutoMapper;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Mappings
{
    public class CreditCardProfile : Profile
    {
        public CreditCardProfile() 
        {
            #region CreditCardProfile

            CreateMap<CreditCard, SaveCreditCardViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore());

            CreateMap<CreditCard, CreditCardViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore());

            #endregion

        }
    }
}
