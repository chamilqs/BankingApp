using AutoMapper;
using BankingApp.Core.Application.ViewModels.Beneficiary;
using BankingApp.Core.Application.ViewModels.Client;
using BankingApp.Core.Application.ViewModels.Loan;
using BankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Application.Mappings
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            #region LoanProfile

            CreateMap<Loan, SaveLoanViewModel>()
                .ReverseMap();

            CreateMap<Loan, LoanViewModel>()
                 .ReverseMap();

            #endregion
        }

    }
}
