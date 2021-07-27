using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.Entities
{
    /// <summary>
    /// Справочная запись
    /// </summary>
    public class BaseCatalog: BaseEntity
    {
        /// <summary>
        /// Имя на русском
        /// </summary>
        public string NameRus { get; set; }

        /// <summary>
        /// Описание на русском
        /// </summary>
        public string DescriptionRus { get; set; }

        /// <summary>
        /// Имя на казахском
        /// </summary>
        public string NameKaz { get; set; }

        /// <summary>
        /// Описание на казахском
        /// </summary>
        public string DescriptionKaz { get; set; }

        /// <summary>
        /// Код записи
        /// </summary>
        public string Code { get; set; }
    }
}
