namespace LawyerService.Entities.Address
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address: BaseEntity
    {
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// Административно-территориальная единица (город, деревня...)
        /// </summary>
        public long AdministrativeTerritoryId { get; set; }
        public AdministrativeTerritory AdministrativeTerritory { get; set; }
        /// <summary>
        /// Элемент улично-дорожной сети (улица, проспект...)
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Дом (строение)
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// Номер квартиры (кабинета, офиса...)
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// Примечание к адресу
        /// </summary>
        public string Notice { get; set; }

    }
}
