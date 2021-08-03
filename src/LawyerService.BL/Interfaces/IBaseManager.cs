using LawyerService.Entities;
using LawyerService.ViewModel.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces
{
    public interface IBaseManager<T, TVM>
        where T : BaseEntity
        where TVM : BaseVM
    {
        Task<List<TVM>> GetAllAsync(bool withDeleted = false);
        Task<TVM> GetByIdAsync(long id, bool withDeleted = false);
        Task<bool> DeleteByIdAsync(long id);
        Task<bool> RestoreByIdAsync(long id);
        Task<bool> CreateOrUpdateAsync(TVM viewModel);
        Task<bool> CreateOrUpdateManyAsync(List<TVM> viewModels);
        Task AddAsync(T entity);
        Task<bool> SaveChangesAsync();
    }
}
