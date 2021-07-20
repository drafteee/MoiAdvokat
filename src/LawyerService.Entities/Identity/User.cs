using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser
    {
        public string Surname { get; set; }

        public UserBalance  Balance { get; set; }
    }
}
