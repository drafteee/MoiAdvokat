using LawyerService.ViewModel.Common;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Transactions
{
    public interface ITransactionManager
    {
        Task<RequestResult> CreateTransactionInService(decimal amount);
        Task<RequestResult> CreateTransactionOutService(decimal amount);
        Task<RequestResult> GetResultOfTransaction(bool isSuccessful, long transactionId);
    }
}
