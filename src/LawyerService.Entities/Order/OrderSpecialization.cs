using LawyerService.Entities.Lawyer;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Специализация заказа
    /// </summary>
    public class OrderSpecialization : BaseEntity
    {
        /// <summary>
        /// FK on Order
        /// </summary>
        public long OrderId { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// FK on Specialization
        /// </summary>
        public long SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
