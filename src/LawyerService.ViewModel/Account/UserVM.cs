using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.ViewModel.Account
{
    public class UserVM
    {
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long SessionId { get; set; }

        public IList<string> Roles { get; set; }
        public IList<string> Functions { get; set; }
    }
}
