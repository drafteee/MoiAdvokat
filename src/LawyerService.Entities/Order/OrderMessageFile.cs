using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    /// <summary>
    /// Прикрепляемые файлы к сообщению
    /// </summary>
    public class OrderMessageFile
    {
        /// <summary>
        /// FK on OrderMessage
        /// </summary>
        public long OrderMessageId { get; set; }
        public OrderMessage OrderMessage { get; set; }

        /// <summary>
        /// FK on Files
        /// </summary>
        public long FileId { get; set; }
        public File File { get; set; }
    }
}
