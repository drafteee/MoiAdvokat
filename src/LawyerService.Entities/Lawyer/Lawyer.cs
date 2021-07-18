using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LawyerService.Entities.Lawyer
{
    //Адвокат(реестр)
    public class Lawyer : BaseEntity
    {
        // Имя
        public string FirstName { get; set; }

        // Отчество
        public string MiddleName { get; set; }

        // Фамилия
        public string LastName { get; set; }

        //Номер лицензии
        public string LicenseNumber { get; set; }

        //Дата выдачи лицензии
        public DateTimeOffset DateOfIssue { get; set; } 

        //FK на Files(копия удостоверения)
        public long FileCopyId { get; set; }

        //FK на Address(Адрес адвоката)
        public long AddressId { get; set; }
        
        //FK на User
        public long UserId { get; set; }

        //Верифицирован ли адвокат в системе
        public bool IsVerified { get; set; }
    }
}
