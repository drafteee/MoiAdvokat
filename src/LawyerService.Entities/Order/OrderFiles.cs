using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Файлы заказа
    /// </summary>
    public class OrderFiles
    {
        /// <summary>
        /// FK on Order
        /// </summary>
        public long OrderId { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// FK on File
        /// </summary>
        public long FileId { get; set; }
        public File File { get; set; }
    }
}
