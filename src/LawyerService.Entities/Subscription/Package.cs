using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    /// <summary>
    /// Пакет функций
    /// </summary>
    public class Package : BaseEntity
    {
        /// <summary>
        /// Имя пакета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость пакета
        /// </summary>
        public double Cost { get; set; }
    }
}
