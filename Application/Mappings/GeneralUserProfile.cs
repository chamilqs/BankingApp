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
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();
            #endregion

        }
    }
}
