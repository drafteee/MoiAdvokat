using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Заказ клиент - адвокат
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// Заголовок заказа
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Описание заказа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Если клиент не хочет указывать настояещее имя
        /// </summary>
        public string NameClient { get; set; }

        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Крайний срок исполнения заказа
        /// </summary>
        public DateTimeOffset EndDueDate { get; set; }

        /// <summary>
        /// FK на Lawyers
        /// </summary>
        public long? LawyerId { get; set; }
        public Lawyer.Lawyer Lawyer { get; set; }

        /// <summary>
        /// Итоговая цена на заказ
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата начала исполнения заказа
        /// </summary>
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата исполнения заказа
        /// </summary>
        public DateTimeOffset FinishDate { get; set; }

        /// <summary>
        /// FK на Statuses(состояние заказа)
        /// </summary>
        public long StatusId { get; set; }
        public OrderStatus Status { get; set; }

        /// <summary>
        /// FK на User(клиент)
        /// </summary>
        public string UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Специализации заказа
        /// </summary>
        public ICollection<OrderSpecialization> OrderSpecializations { get; set; }

        /// <summary>
        /// Процент адвокату
        /// </summary>
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
