using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LawyerService.BL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LawyerService.BL
{
    public class SignalR : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;
        public SignalR(IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }
        public string SetConnection(string userId)
        {
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
            return Context.ConnectionId;
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            await Task.FromResult(0);
        }
    }
}
