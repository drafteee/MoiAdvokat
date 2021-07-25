using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    /// <summary>
    /// Специализация адвоката
    /// </summary>
    public class LawyerSpecialization
    {
        /// <summary>
        /// FK on Specialization
        /// </summary>
        public long SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        /// <summary>
        /// FK on Lawyer
        /// </summary>
        public long LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

    }
}
