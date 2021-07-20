using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities.Identity
{
    public class UserBalance: BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
    }
}
