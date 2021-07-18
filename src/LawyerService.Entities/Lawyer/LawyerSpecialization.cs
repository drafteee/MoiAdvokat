using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Lawyer
{
    // Специализация адвоката
    public class LawyerSpecialization
    {
        //FK on Specialization
        public long SpecializationId { get; set; }

        //FK on Lawyer
        public long LawyerId { get; set; }

    }
}
