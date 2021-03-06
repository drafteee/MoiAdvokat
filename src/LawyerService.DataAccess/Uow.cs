using LawyerService.DataAccess.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.Entities.Address;
using LawyerService.Entities.Chat;
using LawyerService.Entities.Lawyer;
using LawyerService.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.DataAccess
{
    public class Uow : BaseUow<LawyerDbContext>, IUow
    {
        #region Lawyers repositories

        private IGenericRepository<Lawyer> _lawyerRepository;
        private IGenericRepository<Specialization> _specializationRepository;

        #endregion

        private IGenericRepository<Address> _addressRepository;
        private IGenericRepository<AdministrativeTerritory> _administrativeTerritoryRepository;

        #region Orders repositories

        private IGenericRepository<Order> _orderRepository;

        private IGenericRepository<Message> _messageRepository;

        private IGenericRepository<OrderSpecialization> _orderSpecializationRepository;
        private IGenericRepository<OrderStatus> _orderStatusRepository;
        private IGenericRepository<OrderResponse> _orderResponseRepository;

        #endregion

        public Uow(LawyerDbContext context) : base(context)
        {

        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Возвращает запись из таблицы T, наследуемой от <see cref="BaseEntity"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetById<T>(long id, bool withDeleted) where T : BaseEntity
        {
            return _context.Set<T>().Where(x => x.Id == id && (withDeleted || !x.IsDeleted)).FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAll<T>(bool withDeleted) where T : BaseEntity
        {
            return _context.Set<T>().Where(x => withDeleted || !x.IsDeleted).ToListAsync();
        }

        #region Lawyers repositories

        public IGenericRepository<Lawyer> Lawyer => _lawyerRepository ??= new GenericRepository<Lawyer>(_context);
        public IGenericRepository<Specialization> Specializations => _specializationRepository ??= new GenericRepository<Specialization>(_context);

        #endregion

        public IGenericRepository<Address> Address => _addressRepository ??= new GenericRepository<Address>(_context);
        public IGenericRepository<AdministrativeTerritory> AdministrativeTerritory => _administrativeTerritoryRepository ??= new GenericRepository<AdministrativeTerritory>(_context);

        #region Orders repositories

        public IGenericRepository<Order> Orders => _orderRepository ??= new GenericRepository<Order>(_context);

        public IGenericRepository<Message> Messages => _messageRepository ??= new GenericRepository<Message>(_context);
        public IGenericRepository<OrderSpecialization> OrderSpecializations => _orderSpecializationRepository ??= new GenericRepository<OrderSpecialization>(_context);
        public IGenericRepository<OrderStatus> OrderStatuses => _orderStatusRepository ??= new GenericRepository<OrderStatus>(_context);
        public IGenericRepository<OrderResponse> OrderResponses => _orderResponseRepository ??= new GenericRepository<OrderResponse>(_context);

        #endregion
    }
}
