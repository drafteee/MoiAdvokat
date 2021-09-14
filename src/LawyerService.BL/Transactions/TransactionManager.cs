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
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                Date = DateTime.Now,
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
                Date = DateTime.Now,
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
                    var userBalance = await _context.Users.Where(u => u.Id == transaction.UserId).Include(u => u.Balance).Select(u => u.Balance).FirstOrDefaultAsync();
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
                            Date = DateTime.Now,
                            Amount = amount,
                            PreviousBalanceAmount = userBalance.Amount,
                            CurrentBalanceAmount = userBalance.Amount + amount,
                            TransactionReasonId = reasonId,
                            UserId = transaction.UserId
                        };
                        userBalance.Amount += amount;
                    }
                    else
                    {
                        var amount = decimal.Round((transaction.Amount * 100) / (100 - userBalance.ProcentOut), 2);
                        historyUserTransaction = new HistoryUserTransactions()
                        {
                            Date = DateTime.Now,
                            Amount = amount,
                            PreviousBalanceAmount = userBalance.Amount,
                            CurrentBalanceAmount = userBalance.Amount - amount,
                            TransactionReasonId = reasonId,
                            UserId = transaction.UserId
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

        public async Task<RequestResult> PayOrder(long orderId, decimal amount)
        {
            var request = new RequestResult(false, string.Empty);
            var order = await _context.Orders
                .Include(o => o.User)
                    .ThenInclude(u => u.Balance)
                .Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order == null)
            {
                request.Output = _localizationManager.GetString(LocalisationSections.Orders, "NotFound");
                request.ErrorCode = (int)HttpStatusCode.NotFound;
                return request;
            }
            var reasonId = await _context.TransactionReasons.Where(s => s.Code == ((int)TransactionReasonEnum.PayOrder).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (reasonId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundReason");
                return request;
            }
            if (order.User.Balance.Amount < amount)
            {
                request.Output = _localizationManager.GetString(LocalisationSections.Orders, "NotEnoughAmount");
                request.ErrorCode = (int)HttpStatusCode.Forbidden;
                return request;
            }
            var transaction = new HistoryUserTransactions()
            {
                UserId = order.UserId,
                Amount = amount,
                CreatedOn = DateTime.Now,
                Date = DateTime.Now,
                OrderId = orderId,
                PreviousBalanceAmount = order.User.Balance.Amount,
                CurrentBalanceAmount = order.User.Balance.Amount - amount,
                TransactionReasonId = reasonId
            };
            _context.HistoryUserTransactions.Add(transaction);
            order.Price = amount;
            order.User.Balance.Amount -= amount;
            request.Success = _context.SaveChanges() > 0;
            return request;
        }

        public async Task<RequestResult> PaymentOrder(long orderId)
        {
            var request = new RequestResult(false, string.Empty);
            var order = await _context.Orders
                .Include(o => o.Lawyer)
                    .ThenInclude(l => l.User)
                        .ThenInclude(u => u.Balance)
                .Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order == null)
            {
                request.Output = _localizationManager.GetString(LocalisationSections.Orders, "NotFound");
                request.ErrorCode = (int)HttpStatusCode.NotFound;
                return request;
            }
            var reasonId = await _context.TransactionReasons.Where(s => s.Code == ((int)TransactionReasonEnum.SuccessOrder).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (reasonId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundReason");
                return request;
            }
            var transaction = new HistoryUserTransactions()
            {
                UserId = order.Lawyer.UserId,
                Amount = order.Price,
                CreatedOn = DateTime.Now,
                Date = DateTime.Now,
                OrderId = orderId,
                PreviousBalanceAmount = order.Lawyer.User.Balance.Amount,
                CurrentBalanceAmount = order.Lawyer.User.Balance.Amount + order.Price,
                TransactionReasonId = reasonId
            };
            _context.HistoryUserTransactions.Add(transaction);
            order.Lawyer.User.Balance.Amount += order.Price;
            request.Success = _context.SaveChanges() > 0;
            return request;
        }

        public async Task<RequestResult> RefundOrderFromClient(long orderId)
        {
            var request = new RequestResult(false, string.Empty);
            var order = await _context.Orders
                .Include(o => o.User)
                    .ThenInclude(u => u.Balance)
                .Include(o => o.Lawyer)
                    .ThenInclude(l => l.User)
                        .ThenInclude(u => u.Balance)
                .Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order == null)
            {
                request.Output = _localizationManager.GetString(LocalisationSections.Orders, "NotFound");
                request.ErrorCode = (int)HttpStatusCode.NotFound;
                return request;
            }
            var reasonId = await _context.TransactionReasons.Where(s => s.Code == ((int)TransactionReasonEnum.FailOrderByClient).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (reasonId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundReason");
                return request;
            }
            var transactions = new List<HistoryUserTransactions> { new HistoryUserTransactions
            {
                UserId = order.UserId,
                Amount = order.Price * (100 - order.Procent)/100,
                CreatedOn = DateTime.Now,
                Date = DateTime.Now,
                OrderId = orderId,
                PreviousBalanceAmount = order.User.Balance.Amount,
                CurrentBalanceAmount = order.User.Balance.Amount + (order.Price * (100 - order.Procent) / 100),
                TransactionReasonId = reasonId
            }, new HistoryUserTransactions
            {
                UserId = order.Lawyer.UserId,
                Amount = order.Price * order.Procent / 100,
                CreatedOn = DateTime.Now,
                Date = DateTime.Now,
                OrderId = orderId,
                PreviousBalanceAmount = order.Lawyer.User.Balance.Amount,
                CurrentBalanceAmount = order.Lawyer.User.Balance.Amount + (order.Price * order.Procent / 100),
                TransactionReasonId = reasonId
            }};
            _context.HistoryUserTransactions.AddRange(transactions);
            order.User.Balance.Amount += order.Price * (100 - order.Procent) / 100;
            order.Lawyer.User.Balance.Amount += order.Price * order.Procent / 100;
            request.Success = _context.SaveChanges() > 0;
            return request;
        }

        public async Task<RequestResult> RefundOrderFromLawyer(long orderId)
        {
            var request = new RequestResult(false, string.Empty);
            var order = await _context.Orders
                .Include(o => o.User)
                    .ThenInclude(u => u.Balance)
                .Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order == null)
            {
                request.Output = _localizationManager.GetString(LocalisationSections.Orders, "NotFound");
                request.ErrorCode = (int)HttpStatusCode.NotFound;
                return request;
            }
            var reasonId = await _context.TransactionReasons.Where(s => s.Code == ((int)TransactionReasonEnum.FailOrder).ToString()).Select(s => s.Id).FirstOrDefaultAsync();
            if (reasonId == 0)
            {
                request.Message = _localizationManager.GetString(LocalisationSections.HistoryTransactions, "NotFoundReason");
                return request;
            }
            var transaction = new HistoryUserTransactions()
            {
                UserId = order.UserId,
                Amount = order.Price,
                CreatedOn = DateTime.Now,
                Date = DateTime.Now,
                OrderId = orderId,
                PreviousBalanceAmount = order.User.Balance.Amount,
                CurrentBalanceAmount = order.User.Balance.Amount + order.Price,
                TransactionReasonId = reasonId
            };
            _context.HistoryUserTransactions.Add(transaction);
            order.User.Balance.Amount += order.Price;
            request.Success = _context.SaveChanges() > 0;
            return request;
        }
    }
}
