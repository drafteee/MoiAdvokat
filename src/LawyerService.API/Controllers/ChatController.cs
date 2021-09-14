using LawyerService.BL.Interfaces;
using LawyerService.ViewModel.Chat;
using LawyerService.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private IChatManager _manager;
        public ChatController(IChatManager manager) 
        {
            _manager = manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<MessageVM>> GetMessagesByOrder(long orderId)
        {
            return await _manager.GetMessagesByOrder(orderId);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<RequestResult> SendMessage([FromBody] MessageVM message)
        {
            return await _manager.SendMessage(message);
        }
    }
}
