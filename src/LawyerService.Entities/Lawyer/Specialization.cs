using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    /// <summary>
    /// Специализация
    /// </summary>
    public class Specialization : BaseEntity
    {
        /// <summary>
        /// Имя на русском
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя на казахском
        /// </summary>
        public string NameKaz { get; set; }

        /// <summary>
        /// Код специализации
        /// </summary>
        public string Code { get; set; }
    }
}
