using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Order
{
    //Файлы заказа
    public class OrderFiles
    {
        //FL on Order
        public long OrderId { get; set; }
        
        //FK on File
        public long FileId { get; set; }
    }
}
