using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    // Специализация заказа
    public class OrderSpecialization
    {
        //FK on Order
        public long OrderId { get; set; }

        //FK on Specialization
        public long SpecializationId { get; set; }
    }
}
