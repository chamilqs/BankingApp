using AutoMapper;
using BankingApp.Core.Application.Interfaces.Repositories;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.ViewModels.Transaction;
using BankingApp.Core.Domain.Entities;

namespace BankingApp.Core.Application.Services
{
    public class TransactionService : GenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;

        }

    }
}
