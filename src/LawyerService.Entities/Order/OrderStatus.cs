using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Статус Заказа
    /// </summary>
    public class OrderStatus : BaseEntity
    {
        /// <summary>
        /// Имя на русском
        /// </summary>
        public string NameRus { get; set; }

        /// <summary>
        /// Имя на казахском
        /// </summary>
        public string NameKaz { get; set; }

        /// <summary>
        /// Код статуса
        /// </summary>
        public string Code { get; set; }
    }
}
