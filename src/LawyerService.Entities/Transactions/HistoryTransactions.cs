using LawyerService.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerService.Entities.Transactions
{
    /// <summary>
    /// История денежных транзакций в/из сервиса
    /// </summary>
    public class HistoryTransactions: BaseEntity
    {
        /// <summary>
        /// Значение транзакции
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Пользователь, совершающий транзакцию
        /// </summary>
        public string UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// Дата транзакции
        /// </summary>
        [Column(TypeName = "timestamp with time zone")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Транзакция в/из сервиса
        /// </summary>
        public bool IsInService { get; set; }
        /// <summary>
        /// Статус транзакции
        /// </summary>
        public long StatusId { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
