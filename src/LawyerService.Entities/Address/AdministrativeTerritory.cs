namespace LawyerService.Entities.Address
{
    /// <summary>
    /// Административно-территориальная единица (город, деревня...)
    /// </summary>
    public class AdministrativeTerritory: BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип административно-территориальной единицы
        /// </summary>
        public long AdministrativeTerritoryTypeId { get; set; }
        public AdministrativeTerritoryType Type { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
