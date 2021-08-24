using LawyerService.BL.Interfaces.Lawyers;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.API.Controllers.Lawyers
{
    public class SpecializationController : BaseController<ISpecializationManager, Specialization, SpecializationVM>
    {
        public SpecializationController(ISpecializationManager manager) : base(manager)
        {
        }
    }
}
