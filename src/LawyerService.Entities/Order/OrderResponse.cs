using System;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Адвокаты, которые отозвались
    /// </summary>
    public class OrderResponse : BaseEntity
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long LawyerId { get; set; }
        public Lawyer.Lawyer Lawyer { get; set; }

        public decimal Price { get; set; }

        public DateTime[] Dates { get; set; }
    }
}
