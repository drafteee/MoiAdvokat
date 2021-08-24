using LawyerService.ViewModel.Lawyers;
using System.Collections.Generic;

namespace LawyerService.ViewModel.Orders
{
    public class OrderSubmitStarterInfoVM
    {
        public ICollection<SpecializationVM> Specializations { get; set; }
    }
}
