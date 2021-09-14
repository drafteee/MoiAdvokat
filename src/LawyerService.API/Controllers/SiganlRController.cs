using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LawyerService.BL.Interfaces;
using LawyerService.ViewModel;
using Microsoft.AspNetCore.SignalR;
using LawyerService.BL;
using Microsoft.AspNetCore.Authorization;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SiganlRController : ControllerBase
    {
        protected readonly IHubContext<SignalR> _chatHub;
        private readonly IUserConnectionManager _userConnectionManager;

        public SiganlRController(IHubContext<SignalR> chatHub, IUserConnectionManager userConnectionManager)
        {
            _chatHub = chatHub;
            _userConnectionManager = userConnectionManager;
        }

        private const long MaxFileSize = 15L * 1024L * 1024L; // 15MB

        [HttpPost("sendMessage")]
        [DisableRequestSizeLimit]   
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [AllowAnonymous]
        public async Task<bool> SendMessage(string test)
        {
            //var connections = _userConnectionManager.GetUserConnections(command.Recipient);
            //if (connections != null && connections.Count > 0)
            //{
            //    foreach (var connectionId in connections)
            //    {
            //        await _chatHub.Clients.Client(connectionId).SendAsync("sendMessages", command.Sender, command.Recipient);
            //        await _chatHub.Clients.Client(connectionId).SendAsync("getNotification");
            //    }
            //}
            await _chatHub.Clients.All.SendAsync("sendMessages", test);
            return true;
        }

    }
}
