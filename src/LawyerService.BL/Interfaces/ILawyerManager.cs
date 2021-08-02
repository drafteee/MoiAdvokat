using System.Collections.Generic;
using System.Threading.Tasks;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel;

namespace LawyerService.BL.Interfaces
{
    public interface ILawyerManager : IBaseManager<Lawyer, LawyerVM>
    {
    }
}
