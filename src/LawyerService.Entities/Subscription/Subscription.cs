using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    /// <summary>
    /// Подписка пользователя
    /// </summary>
    public class Subscription : BaseEntity
    {
        /// <summary>
        /// FK на User 
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Дата начала подписки
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания подписки
        /// </summary>
        public DateTimeOffset FinishDate { get; set; }

        /// <summary>
        /// FK на Package
        /// </summary>
        public long PackageId { get; set; }
        public Package Package { get; set; }

        /// <summary>
        /// Активна ли подписка(worker будет проверять дату начала и конца)
        /// </summary>
        public bool IsActive { get; set; }
    }
}
