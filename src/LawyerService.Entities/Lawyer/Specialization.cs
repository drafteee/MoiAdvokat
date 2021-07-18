using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    //Специализация
    public class Specialization : BaseEntity
    {
        //Иям на русском
        public string Name { get; set; }

        //Иям на казахском
        public string NameKaz { get; set; }

        //Код специализации
        public string Code { get; set; }
    }
}
