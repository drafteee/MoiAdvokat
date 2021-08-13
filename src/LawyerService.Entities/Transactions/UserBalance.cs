using LawyerService.Entities.Identity;
using System.Diagnostics.CodeAnalysis;

namespace LawyerService.Entities.Transactions
{
    /// <summary>
    /// Денежный баланс пользователя в системе
    /// </summary>
    public class UserBalance: BaseEntity
    {
        /// <summary>
        /// Значение баланса
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Забираемый процент с суммы взноса денег в систему
        /// </summary>
        public byte ProcentIn { get; set; }
        /// <summary>
        /// Забираемый процент с суммы выплаты денег из системы
        /// </summary>
        public byte ProcentOut { get; set; }
    }
}
