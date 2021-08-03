using LawyerService.BL.Interfaces;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.API.Controllers
{
    public class LawyerController : BaseController<ILawyerManager, Lawyer, LawyerVM>
    {
        public LawyerController(ILawyerManager manager) : base(manager)
        {
        }
    }
}
