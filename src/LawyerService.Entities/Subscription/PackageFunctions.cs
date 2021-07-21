using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Subscription
{
    /// <summary>
    /// m2m package and functions
    /// </summary>
    public class PackageFunctions
    {
        /// <summary>
        /// FK на Functions
        /// </summary>
        public long FunctionId { get; set; }
        //public Function Function { get; set; }

        /// <summary>
        /// FK на Package
        /// </summary>
        public long PackageId { get; set; }
        public Package Package { get; set; }

        /// <summary>
        /// Сколько можно эту функцию применять
        /// </summary>
        public byte CountAction { get; set; }
    }
}
