using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyerService.BL.Interfaces;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.ViewModel;
using System;

namespace LawyerService.BL
{
    public class UserConnectionManager : IUserConnectionManager
    {
        private static Dictionary<string, List<string>> userConnectionMap = new Dictionary<string, List<string>>();
        private static string userConnectionMapLocker = string.Empty;

        public void KeepUserConnection(string userId, string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.ContainsKey(userId))
                {
                    userConnectionMap[userId] = new List<string>();
                }
                userConnectionMap[userId].Add(connectionId);
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                var listUsers = userConnectionMap.Keys;
                foreach (var userId in listUsers)
                {
                    if (userConnectionMap.ContainsKey(userId) && userConnectionMap[userId].Contains(connectionId))
                    {
                        userConnectionMap[userId].Remove(connectionId);
                        if (userConnectionMap[userId].Count == 0)
                            userConnectionMap.Remove(userId);
                        break;
                    }
                }
            }
        }

        public List<string> GetUserConnections(string userId)
        {
            var conn = new List<string>();
            if (string.IsNullOrEmpty(userId))
                return null;
            lock (userConnectionMapLocker)
            {
                if (userConnectionMap.ContainsKey(userId))
                    conn = userConnectionMap[userId];
                else
                    conn = null;
            }
            return conn;
        }
    }
}
