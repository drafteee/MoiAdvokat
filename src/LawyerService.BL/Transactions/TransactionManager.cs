using AutoMapper;
using LawyerService.BL.Enums.TransactionStatus;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Transactions;
using LawyerService.DataAccess;
using LawyerService.Entities.Transactions;
using LawyerService.Resources;
using LawyerService.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Transactions
{
    public class TransactionManager : ITransactionManager
    {

        private readonly LawyerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILocalizationManager _localizationManager;
        private readonly IUserAccessor _userAccessor;

        public TransactionManager(LawyerDbContext context, IMapper mapper, ILocalizationManager localizationManager, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _localizationManager = localizationManager;
            _userAccessor = userAccessor;
        }

        public async Task<RequestResult> CreateTransactionInService(decimal amount)
        {
            var request = new RequestResult(false, string.Empty);
            var user = await _context.Users.Where(u => u.UserName == _userAccessor.GetCurrentUsername())
                    .Include(u => u.Balance)
                    .FirstOrDefaultAsync();
            var isAnyTransactions = await _context.HistoryTransactions.AnyAsync(t => t.Status.Code == ((int)TransactionStatusEnum.InProgress).ToString());
            if (isAnyTransactions)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "AnyTransactionsInProgress");
                return request;
            }
            var statusId = await _context.TransactionStatuses.Where(s => s.Code == ((int)TransactionStatusEnum.InProgress).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (statusId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundStatus");
                return request;
            }
            var transactionAmount = decimal.Round((amount * (100 + user.Balance.ProcentIn)) / 100, 2);
            var transaction = new HistoryTransactions()
            {
                Date = DateTimeOffset.Now,
                UserId = user.Id,
                IsInService = true,
                Amount = transactionAmount,
                StatusId = statusId,
            };
            _context.HistoryTransactions.Add(transaction);
            request.Success = _context.SaveChanges() > 0;
            return request;
        }

        public async Task<RequestResult> CreateTransactionOutService(decimal amount)
        {
            var request = new RequestResult(false, string.Empty);
            var user = await _context.Users.Where(u => u.UserName == _userAccessor.GetCurrentUsername())
                .Include(u => u.Balance)
                .FirstOrDefaultAsync();
            if (user.Balance.Amount < amount)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotEnoughAmountInBalance");
                return request;
            }
            var isAnyTransactions = await _context.HistoryTransactions.AnyAsync(t => t.Status.Code == ((int)TransactionStatusEnum.InProgress).ToString());
            if (isAnyTransactions)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "AnyTransactionsInProgress");
                return request;
            }
            var statusId = await _context.TransactionStatuses.Where(s => s.Code == ((int)TransactionStatusEnum.InProgress).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (statusId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundStatus");
                return request;
            }
            var transactionAmount = decimal.Round((amount * (100 - user.Balance.ProcentOut)) / 100, 2);
            var transaction = new HistoryTransactions()
            {
                Date = DateTimeOffset.Now,
                UserId = user.Id,
                IsInService = false,
                Amount = transactionAmount,
                StatusId = statusId,
            };
            _context.HistoryTransactions.Add(transaction);
            request.Success = _context.SaveChanges() > 0;
            return request;
        }

        public async Task<RequestResult> GetResultOfTransaction(bool isSuccessful, long transactionId)
        {
            var request = new RequestResult(false, string.Empty);
            var transaction = await _context.HistoryTransactions.Where(t => t.Id == transactionId && t.Status.Code == ((int)TransactionStatusEnum.InProgress).ToString()).FirstOrDefaultAsync();
            if (transaction != null)
            {
                var statusId = await _context.TransactionStatuses.Where(s => s.Code == ((int)(isSuccessful ? TransactionStatusEnum.IsSuccessful : TransactionStatusEnum.IsUnsuccessful)).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
                if (statusId == 0)
                {
                    request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundStatus");
                    return request;
                }
                transaction.StatusId = statusId; 
                if (isSuccessful)
                {
                    var userBalance = await _context.UserBalances.Where(u => u.UserId == transaction.UserId).FirstOrDefaultAsync();
                    HistoryUserTransactions historyUserTransaction = new();
                    var reasonId = await _context.TransactionReasons.Where(s => s.Code == ((int)(transaction.IsInService ? TransactionReasonEnum.Input : TransactionReasonEnum.Output)).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
                    if (reasonId == 0)
                    {
                        request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundReason");
                        return request;
                    }
                    if (transaction.IsInService)
                    {
                        var amount = decimal.Round((transaction.Amount * 100) / (100 + userBalance.ProcentIn), 2);
                        historyUserTransaction = new HistoryUserTransactions()
                        {
                            Date = DateTimeOffset.Now,
                            Amount = amount,
                            PreviousBalanceAmount = userBalance.Amount,
                            CurrentBalanceAmount = userBalance.Amount + amount,
                            TransactionReasonId = reasonId,
                            UserBalanceId = userBalance.Id
                        };
                        userBalance.Amount += amount;
                    }
                    else
                    {
                        var amount = decimal.Round((transaction.Amount * 100) / (100 - userBalance.ProcentOut), 2);
                        historyUserTransaction = new HistoryUserTransactions()
                        {
                            Date = DateTimeOffset.Now,
                            Amount = amount,
                            PreviousBalanceAmount = userBalance.Amount,
                            CurrentBalanceAmount = userBalance.Amount - amount,
                            TransactionReasonId = reasonId,
                            UserBalanceId = userBalance.Id
                        };
                        userBalance.Amount -= amount;
                        if (userBalance.Amount < 0)
                            userBalance.Amount = 0;
                    }
                    _context.UserBalances.Update(userBalance);
                    _context.HistoryUserTransactions.Add(historyUserTransaction);
                    request.Success = _context.SaveChanges() > 0;
                }
                else
                {
                    request.Output = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "UnsuccessfulTransction");
                }
            }
            return request;
        }
    }
}
