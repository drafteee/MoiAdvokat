using System.Collections.Generic;
using System.Threading.Tasks;
using LawyerService.ViewModel;

namespace LawyerService.BL.Interfaces
{
    public interface ILawyerManager
    {
        Task<ICollection<LawyerVM>> GetAllAsync();
        Task<LawyerVM> GetByIDAsync(int representativeId);
        Task<bool> CreateOrUpdate(LawyerVM lawyer);
    }
}
