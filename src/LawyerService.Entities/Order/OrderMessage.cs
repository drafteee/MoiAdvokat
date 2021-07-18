using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    //Сообщения в заказе
    public class OrderMessage : BaseEntity
    {
        //FK on Order
        public long OrderId { get; set; }

        //FK on Lawyer
        public long LawyerId { get; set; }

        //FK on User
        public long ClientId { get; set; }

        //Сообщение
        public string Content { get; set; }

    }
}
