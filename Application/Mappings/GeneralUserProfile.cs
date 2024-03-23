using AutoMapper;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Application.DTOs.Account;

namespace BankingApp.Core.Application.Mappings
{
    public class GeneralUserProfile : Profile
    {
        public GeneralUserProfile()
        {
            #region UserProfileIdentity
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();
            #endregion

        }
    }
}
