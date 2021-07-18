using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    //Прикрепляемые файлы к сообщению
    public class OrderMessageFile
    {
        //FK on OrderMessage
        public long OrderMessagId { get; set; }

        //FK on Files
        public long FileId { get; set; }
    }
}
