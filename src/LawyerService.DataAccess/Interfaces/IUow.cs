using LawyerService.Entities.Address;
using LawyerService.Entities.Lawyer;
using Microsoft.EntityFrameworkCore;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IUow : IBaseUow
    {
        IGenericRepository<Lawyer> Lawyer { get; }
        IGenericRepository<Address> Address { get; }
        DbSet<T> Set<T>() where T : class;
    }
}
