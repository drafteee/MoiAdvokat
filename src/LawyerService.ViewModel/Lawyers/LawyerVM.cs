using LawyerService.ViewModel.Address;
using LawyerService.ViewModel.Base;
using System;

namespace LawyerService.ViewModel.Lawyers
{
    public class LawyerVM : BaseVM
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Номер лицензии
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Дата выдачи лицензии
        /// </summary>
        public DateTimeOffset DateOfIssue { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public AddressVM Address { get; set; }
        public long AddressId { get; set; }

        /// <summary>
        /// Название файла с удостоверением
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Id файла с удостоверением
        /// </summary>
        public long FileId { get; set; }

    }
}
