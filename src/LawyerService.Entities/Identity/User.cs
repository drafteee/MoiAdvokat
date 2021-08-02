using LawyerService.Entities.Transactions;
using LawyerService.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser, IEntity
    {
        public string Surname { get; set; }

        public UserBalance Balance { get; set; }
    }
}
