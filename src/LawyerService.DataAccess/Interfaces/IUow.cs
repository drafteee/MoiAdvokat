using LawyerService.Entities;
using LawyerService.Entities.Address;
using LawyerService.Entities.Lawyer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IUow : IBaseUow
    {
        IGenericRepository<Lawyer> Lawyer { get; }
        IGenericRepository<Address> Address { get; }
        DbSet<T> Set<T>() where T : class;
        Task<T> GetById<T>(long id, bool withDeleted) where T : BaseEntity;
        Task<List<T>> GetAll<T>(bool withDeleted) where T : BaseEntity;
    }
}
