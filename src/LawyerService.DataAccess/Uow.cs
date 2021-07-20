using LawyerService.DataAccess.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;

namespace LawyerService.DataAccess
{
    public class Uow : BaseUow<LawyerDbContext>, IUow
    {
        private IGenericRepository<Lawyer> _lawyerRepository;

        public Uow(LawyerDbContext context) : base(context)
        {
        }

        public IGenericRepository<Lawyer> Lawyer => _lawyerRepository ??= new GenericRepository<Lawyer>(_context);
    }
}
