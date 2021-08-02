using System;

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
        public long UserBalanceId { get; set; }
        public UserBalance UserBalance { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTimeOffset Date { get; set; }

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
