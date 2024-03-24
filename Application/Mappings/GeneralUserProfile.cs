using AutoMapper;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Dtos.Account;

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
                .ForMember(dest => dest.AccountAmount, opt => opt.Ignore())
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserViewModel>()
                .ForMember(dest => dest.AccountAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

        }
    }
}
