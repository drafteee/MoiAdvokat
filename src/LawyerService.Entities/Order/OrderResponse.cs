using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Адвокаты, которые отозвались
    /// </summary>
    public class OrderResponse : BaseEntity
    {
        public long OrderId { get; set; }
        public long LawyerId { get; set; }

        public decimal Price { get; set; }
        public bool IsChoosen{ get; set; }

        public DateTime[] Dates { get; set; }
    }
}
