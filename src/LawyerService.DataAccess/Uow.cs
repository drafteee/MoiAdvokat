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
        private IGenericRepository<Lawyer> _lawyerRepository;
        private IGenericRepository<Address> _addressRepository;
        private IGenericRepository<Order> _orderRepository;

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

        public IGenericRepository<Lawyer> Lawyer => _lawyerRepository ??= new GenericRepository<Lawyer>(_context);

        public IGenericRepository<Address> Address => _addressRepository ??= new GenericRepository<Address>(_context);
        public IGenericRepository<Order> Orders => _orderRepository ??= new GenericRepository<Order>(_context);
    }
}
