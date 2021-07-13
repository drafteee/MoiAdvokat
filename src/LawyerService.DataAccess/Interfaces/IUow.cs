using LawyerService.Entities;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IUow : IBaseUow
    {
        IGenericRepository<Lawyer> Lawyer { get; }

    }
}
