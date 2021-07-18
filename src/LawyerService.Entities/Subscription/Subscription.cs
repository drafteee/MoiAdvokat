using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    //Подписка пользователя
    public class Subscription : BaseEntity
    {
        //FK на User 
        public long UserId { get; set; }

        //Дата начала подписки
        public DateTimeOffset StartDate { get; set; }

        //Дата окончания подписки
        public DateTimeOffset FinishDate { get; set; }

        //FK на Package
        public long PackageId { get; set; }

        //Активна ли подписка(worker будет проверять дату начала и конца)
        public bool IsActive { get; set; }
    }
}
