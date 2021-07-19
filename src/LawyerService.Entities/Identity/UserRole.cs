using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserRole : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
