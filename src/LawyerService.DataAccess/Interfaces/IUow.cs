using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IUow : IBaseUow
    {
        IGenericRepository<Lawyer> Lawyer { get; }

    }
}
