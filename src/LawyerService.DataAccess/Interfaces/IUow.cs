using LawyerService.Entities;
using LawyerService.Entities.Address;
using LawyerService.Entities.Lawyer;
using LawyerService.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IUow : IBaseUow
    {
        #region Lawyers repositories

        IGenericRepository<Lawyer> Lawyer { get; }
        IGenericRepository<Specialization> Specializations { get; }

        #endregion

        #region Address repositories

        IGenericRepository<Address> Address { get; }
        IGenericRepository<AdministrativeTerritory> AdministrativeTerritory { get; }

        #endregion

        #region Orders repositories

        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderSpecialization> OrderSpecializations { get; }
        IGenericRepository<OrderStatus> OrderStatuses { get; }
        IGenericRepository<OrderResponse> OrderResponses { get; }

        #endregion

        DbSet<T> Set<T>() where T : class;
        Task<T> GetById<T>(long id, bool withDeleted) where T : BaseEntity;
        Task<List<T>> GetAll<T>(bool withDeleted) where T : BaseEntity;
    }
}
