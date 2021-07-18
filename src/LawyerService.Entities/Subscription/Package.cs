using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    //Пакет функций
    public class Package : BaseEntity
    {
        //Имя пакета
        public string Name { get; set; }

        //Стоимость пакета
        public double Cost { get; set; }
    }
}
