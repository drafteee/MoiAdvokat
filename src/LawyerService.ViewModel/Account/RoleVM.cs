using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.ViewModel.Account
{
    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<FunctionVM> Functions { get; set; }
    }
}
