using LawyerService.ViewModel.Common;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Transactions
{
    public interface ITransactionManager
    {
        Task<RequestResult> CreateTransactionInOutService(bool IsInService);
    }
}
