using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Функции пользователя
    /// </summary>
    public class Function : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<RoleFunction> RoleFunctions { get; set; }
    }
}
