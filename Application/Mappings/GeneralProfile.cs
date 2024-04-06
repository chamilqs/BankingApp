using AutoMapper;
using BankingApp.Core.Application.Dtos.Account;
using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.CreditCard;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Application.ViewModels.SavingsAccount;
using BankingApp.Core.Application.ViewModels.Transaction;
using BankingApp.Core.Application.ViewModels.User;
using BankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
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

            #region ClientProfile

            CreateMap<Client, SaveClientViewModel>()
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

            #region CreditCardProfile

            CreateMap<CreditCard, SaveCreditCardViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore());

            CreateMap<CreditCard, CreditCardViewModel>()
                .ReverseMap()
                .ForMember(c => c.Client, opt => opt.Ignore());

            #endregion

            #region UserProfile
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

            #region LoanProfile

            CreateMap<Loan, SaveLoanViewModel>()
                .ReverseMap();

            CreateMap<Loan, LoanViewModel>()
                 .ReverseMap();

            #endregion

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

            #region TransactionProfile
            CreateMap<Transaction, SaveTransactionViewModel>()
                .ReverseMap()
                .ForMember(x => x.TransactionType, opt => opt.Ignore());

            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(x => x.TransactionTypeName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.TransactionType, opt => opt.Ignore());
            #endregion

        }

    }
}
