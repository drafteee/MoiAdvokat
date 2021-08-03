using LawyerService.DataAccess.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Address;
using LawyerService.Entities.Lawyer;
using Microsoft.EntityFrameworkCore;

namespace LawyerService.DataAccess
{
    public class Uow : BaseUow<LawyerDbContext>, IUow
    {
        private IGenericRepository<Lawyer> _lawyerRepository;
        private IGenericRepository<Address> _addressRepository;

        public Uow(LawyerDbContext context) : base(context)
        {

        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IGenericRepository<Lawyer> Lawyer => _lawyerRepository ??= new GenericRepository<Lawyer>(_context);

        public IGenericRepository<Address> Address => _addressRepository ??= new GenericRepository<Address>(_context);
    }
}
