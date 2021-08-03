using System.Collections.Generic;
using System.Threading.Tasks;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.BL.Interfaces
{
    public interface ILawyerManager : IBaseManager<Lawyer, LawyerVM>
    {
    }
}
