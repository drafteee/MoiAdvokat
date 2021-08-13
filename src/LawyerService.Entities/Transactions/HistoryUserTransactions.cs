using LawyerService.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerService.Entities.Transactions
{
    /// <summary>
    /// История денежных транзакций внутри сервиса
    /// </summary>
    public class HistoryUserTransactions : BaseEntity 
    {
        /// <summary>
        /// Значение транзакции
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Причина изменения баланса
        /// </summary>
        public long TransactionReasonId { get; set; }
        public TransactionReason Reason { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        [Column(TypeName = "timestamp with time zone")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Предыдущее значение баланса
        /// </summary>
        public decimal PreviousBalanceAmount { get; set; }

        /// <summary>
        /// Текущее значение транзакции
        /// </summary>
        public decimal CurrentBalanceAmount { get; set; }

        /// <summary>
        /// Заказ пользователя
        /// </summary>
        public long? OrderId { get; set; }
        public Order.Order Order { get; set; }
    }
}
