using System.Threading.Tasks;
using LawyerService.ViewModel.Account;
using LawyerService.ViewModel.Common;

namespace LawyerService.BL.Interfaces.Account
{
    public interface IUserManager
    {
        Task<RequestResult> CreateUserAsync(UserVM userVM, string password);
        Task<RequestResult> LoginAsync(string userName, string password, string ip, string userAgent);
    }
}
