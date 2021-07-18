using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    //Статус Заказа
    public class OrderStatus : BaseEntity
    {
        //Имя на русском
        public string NameRus { get; set; }

        //Имя на казахском
        public string NameKaz { get; set; }

        // Код статуса
        public string Code { get; set; }
    }
}
