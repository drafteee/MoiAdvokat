using LawyerService.ViewModel.Account;
using LawyerService.ViewModel.Base;
using LawyerService.ViewModel.Lawyers;
using System;

namespace LawyerService.ViewModel.Orders
{
    public class OrderVM : BaseVM
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
        public LawyerVM Lawyer { get; set; }

        /// <summary>
        /// Итоговая цена на заказ
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата начала исполнения заказа
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата исполнения заказа
        /// </summary>
        public DateTimeOffset FinishDate { get; set; }

        /// <summary>
        /// FK на Statuses(состояние заказа)
        /// </summary>
        public long StatusId { get; set; }
        public OrderStatusVM Status { get; set; }

        /// <summary>
        /// FK на User(клиент)
        /// </summary>
        public string UserId { get; set; }
        public UserVM User { get; set; }
    }
}
