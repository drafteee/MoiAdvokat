using LawyerService.ViewModel.Base;

namespace LawyerService.ViewModel.Address
{
    public class AddressVM : BaseVM
    {
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public CountryVM Country { get; set; }
        public long CountryId { get; set; }

        /// <summary>
        /// Административно-территориальная единица (город, деревня...)
        /// </summary>
        public AdministrativeTerritoryVM AdministrativeTerritory { get; set; }
        public long AdministrativeTerritoryId { get; set; }

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
