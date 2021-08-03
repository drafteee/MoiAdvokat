using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.ViewModel.Account
{
    public class ActiveSessionVM
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string UserAgent { get; set; }
        public string IP { get; set; }
        public bool IsCurrent { get; set; }
    }
}
