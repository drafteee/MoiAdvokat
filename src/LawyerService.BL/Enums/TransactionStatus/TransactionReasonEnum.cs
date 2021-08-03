using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL.Enums.TransactionStatus
{
    /// <summary>
    /// Коды причин изменения денежного баланса пользовтеля внутри сервисе
    /// </summary>
    public enum TransactionReasonEnum
    {
        /// <summary>
        /// Ввод денежных средств на баланс
        /// </summary>
        Input = 1,
        /// <summary>
        /// Вывод денежных средств из баланса
        /// </summary>
        Output,
        /// <summary>
        /// Оплата заказа
        /// </summary>
        PayOrder,
        /// <summary>
        /// Успешное выполнение заказа
        /// </summary>
        SuccessOrder,
        /// <summary>
        /// Невыполнение заказа адвокатом
        /// </summary>
        FailOrder,
        /// <summary>
        /// Невыполнение заказа по инициативе клиента
        /// </summary>
        FailOrderByClient
    }
}
