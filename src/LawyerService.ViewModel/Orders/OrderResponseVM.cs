using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.ViewModel.Orders
{
    public class OrderResponseVM : BaseVM
    {
        public long OrderId { get; set; }
        public long LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

        public decimal Price { get; set; }

        public DateTime[] Dates { get; set; }
    }
}
