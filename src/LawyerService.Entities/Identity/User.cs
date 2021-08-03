using LawyerService.Entities.Transactions;
using LawyerService.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser, IEntity
    {
        public string Surname { get; set; }
        public UserBalance Balance { get; set; }

        public virtual IEnumerable<Function> Functions { get; set; }

    }
}
