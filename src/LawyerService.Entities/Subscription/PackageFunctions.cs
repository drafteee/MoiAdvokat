using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    //m2m package and functions
    public class PackageFunctions
    {
        //FK на Functions
        public long FunctionId { get; set; }
        //FK на Package
        public long PackageId { get; set; }

        //Сколько можно эту функцию применять
        public byte CountAction { get; set; }
    }
}
