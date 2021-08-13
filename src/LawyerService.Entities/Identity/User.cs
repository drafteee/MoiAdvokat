using LawyerService.Entities.Transactions;
using LawyerService.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser, IEntity
    {
        public string Surname { get; set; }

        public long BalanceId { get; set; }
        public UserBalance Balance { get; set; }

        public long? AddressId { get; set; }
        public Address.Address Address { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual IEnumerable<Function> Functions { get; set; }

    }
}
