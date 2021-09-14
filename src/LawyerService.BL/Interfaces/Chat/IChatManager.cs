using System.Collections.Generic;
using System.Threading.Tasks;
using LawyerService.Entities.Chat;
using LawyerService.ViewModel.Chat;
using LawyerService.ViewModel.Common;

namespace LawyerService.BL.Interfaces
{
    public interface IChatManager
    {
        Task<IEnumerable<MessageVM>> GetMessagesByOrder(long orderId);
        Task<RequestResult> SendMessage(MessageVM message);
    }
}
