using LawyerService.DataAccess;

namespace LawyerService.BL.Service
{
    public class BaseService
    {
        public readonly LawyerDbContext _db;

        public BaseService()
        {

        }

        public BaseService(LawyerDbContext db)
        {
            _db = db;
        }
    }
}
