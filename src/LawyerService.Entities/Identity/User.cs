using LawyerService.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser, IEntity
    {
        public string Surname { get; set; }
    }
}
