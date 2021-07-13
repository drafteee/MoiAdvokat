using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LawyerService.Entities
{
    public class Lawyer : BaseEntity
    {
        [Key]
        public int LawyerId { get; set; }
    }
}
