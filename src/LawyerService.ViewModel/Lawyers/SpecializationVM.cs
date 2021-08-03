using LawyerService.ViewModel.Base;

namespace LawyerService.ViewModel.Lawyers
{
    public class SpecializationVM : BaseVM
    {
        /// <summary>
        /// Имя на русском
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя на казахском
        /// </summary>
        public string NameKaz { get; set; }

        /// <summary>
        /// Код специализации
        /// </summary>
        public string Code { get; set; }
    }
}
