using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    //Оценка адвокату
    public class ReviewLawyer : BaseEntity
    {
        //FK on Lawyer
        public long LawyerId { get; set; }

        //FK on User
        public long ClientId { get; set; }

        //Отзыв
        public string Description { get; set; }

        //Оценка
        public byte Assesment { get; set; }
    }
}
