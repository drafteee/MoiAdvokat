using LawyerService.ViewModel.Base;

namespace LawyerService.ViewModel.Address
{
    public class AdministrativeTerritoryVM : BaseVM
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип административно-территориальной единицы
        /// </summary>
        public AdministrativeTerritoryTypeVM Type { get; set; }
        public long AdministrativeTerritoryTypeId { get; set; }
    }
}
