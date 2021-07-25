using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    /// <summary>
    /// Оценка адвокату
    /// </summary>
    public class ReviewLawyer : BaseEntity
    {
        /// <summary>
        /// FK on Lawyer
        /// </summary>
        public long LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

        /// <summary>
        /// FK on User
        /// </summary>
        public long ClientId { get; set; }
        public User Client { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public byte Assesment { get; set; }
    }
}
