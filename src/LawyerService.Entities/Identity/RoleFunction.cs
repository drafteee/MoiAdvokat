using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Смежная таблица
    /// </summary>
    public class RoleFunction : BaseEntity
    {
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
        public long FunctionId { get; set; }
        public virtual Function Function { get; set; }
    }
}
