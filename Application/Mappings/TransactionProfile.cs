using AutoMapper;
using BankingApp.Core.Application.ViewModels.Transaction;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, SaveTransactionViewModel>()
                .ReverseMap()
                .ForMember(x => x.TransactionType, opt => opt.Ignore());

            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(x => x.TransactionTypeName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.TransactionType, opt => opt.Ignore());             

        }
    }
}
