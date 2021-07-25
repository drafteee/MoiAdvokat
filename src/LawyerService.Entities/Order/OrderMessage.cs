using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Сообщения в заказе
    /// </summary>
    public class OrderMessage : BaseEntity
    {
        /// <summary>
        /// FK on Order
        /// </summary>
        public long OrderId { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// FK on Lawyer
        /// </summary>
        public long LawyerId { get; set; }
        public Lawyer.Lawyer Lawyer { get; set; }

        /// <summary>
        /// FK on User
        /// </summary>
        public User ClientId { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Content { get; set; }
    }
}
