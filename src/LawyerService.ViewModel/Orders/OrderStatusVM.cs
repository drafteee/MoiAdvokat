using LawyerService.ViewModel.Base;

namespace LawyerService.ViewModel.Orders
{
    public class OrderStatusVM : BaseVM
    {
        /// <summary>
        /// Имя на русском
        /// </summary>
        public string NameRus { get; set; }

        /// <summary>
        /// Имя на казахском
        /// </summary>
        public string NameKaz { get; set; }

        /// <summary>
        /// Код статуса
        /// </summary>
        public string Code { get; set; }
    }
}
