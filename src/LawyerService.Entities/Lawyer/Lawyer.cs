using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LawyerService.Entities.Lawyer
{
    /// <summary>
    /// Адвокат(реестр)
    /// </summary>
    public class Lawyer : BaseEntity
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
        /// FK на Files(копия удостоверения)
        /// </summary>
        public long FileCopyId { get; set; }
        public File FileCopy { get; set; }

        /// <summary>
        /// FK на Address(Адрес адвоката)
        /// </summary>
        public long AddressId { get; set; }
        public Address.Address Address { get; set; }

        /// <summary>
        /// FK на User
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Верифицирован ли адвокат в системе
        /// </summary>
        public bool IsVerified { get; set; }
    }
}
