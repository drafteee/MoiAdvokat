using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    //Заказ клиент - адвокат
    public class Order : BaseEntity
    {
        //Заголовок заказа
        public string Header { get; set; }

        //Описание заказа
        public string Description { get; set; }

        //Если клиент не хочет указывать настояещее имя
        public string NameClient { get; set; }

        //Телефон клиента
        public string PhoneNumber { get; set; }

        //Крайний срок исполнения заказа
        public DateTimeOffset EndDueDate { get; set; }

        //FK на Lawyers
        public long? LawyerId { get; set; }

        //Итоговая цена на заказ
        public double Price { get; set; }

        //Дата начала исполнения заказа
        public DateTimeOffset StartDate { get; set; }

        //Дата исполнения заказа
        public DateTimeOffset FinishDate { get; set; }
        
        //FK на Statuses(состояние заказа)
        public long StatusId { get; set; }
        
        //FK на User(клиент)
        public long UserId { get; set; }

        //Процент адвокату
        private sbyte _procent;
        public sbyte Procent 
        {
            get { return _procent; } 
            set { 
                if(value > 100 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Значение не может быть больше 100 и меньше 0");
                }
                _procent = value;
            } 
        }
    }
}
