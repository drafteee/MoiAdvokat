using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.BL.Interfaces
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);
    }
}
