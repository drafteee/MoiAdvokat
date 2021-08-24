using LawyerService.DataAccess.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.Entities.Address;
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

        #region Orders repositories
        
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderSpecialization> _orderSpecializationRepository;
        private IGenericRepository<OrderStatus> _orderStatusRepository;
        
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
            return _context.Set<T>().Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

        #region Orders repositories

        public IGenericRepository<Order> Orders => _orderRepository ??= new GenericRepository<Order>(_context);
        public IGenericRepository<OrderSpecialization> OrderSpecializations => _orderSpecializationRepository ??= new GenericRepository<OrderSpecialization>(_context);
        public IGenericRepository<OrderStatus> OrderStatuses => _orderStatusRepository ??= new GenericRepository<OrderStatus>(_context);

        #endregion
    }
}
