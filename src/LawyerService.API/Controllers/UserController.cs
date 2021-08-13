using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LawyerService.BL.Interfaces;
using LawyerService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using LawyerService.BL.Interfaces.Account;
using LawyerService.ViewModel.Common;
using LawyerService.ViewModel.Account;
using LawyerService.ViewModel.Address;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<RequestResult> CreateUser(string password, [FromBody] UserVM userVM)
        {
            return _userManager.CreateUserAsync(userVM, password);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<RequestResult> Login([FromBody] LoginVM userVM)
        {
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return await _userManager.LoginAsync(userVM.UserName, userVM.Password, ip, userAgent);
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<RequestResult> CreateRole(string role)
        {
            return await _userManager.CreateRoleAsync(role);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<RequestResult> AssignRoleToUser(string userName, string role)
        {
            return await _userManager.AssignRoleToUserAsync(userName, role);
        }

        [HttpGet]
        public async Task<RequestResult> RefreshUserData()
        {
            return await _userManager.RefreshUserDataAsync();
        }

        [HttpPost]
        public async Task<RequestResult> Logout(long SessionId)
        {
            return await _userManager.LogoutAsync(SessionId);
        }

        [HttpPost]
        public async Task<RequestResult> Refresh(string token,  long sessionId)
        {
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return await _userManager.RefreshAsync(token, ip, userAgent, sessionId);
        }

        [HttpGet]
        public async Task<RequestResult> GetActiveSessions(string userId, long sessionId)
        {
            return await _userManager.GetActiveSessionsAsync(userId, sessionId);
        }

        [HttpPost]
        public async Task<RequestResult> RemoveSession(long sessionId)
        {
            return await _userManager.RemoveSessionAsync(sessionId);
        }

        [HttpGet]
        public async Task<RequestResult> GetUserFunctions(string userId)
        {
            return await _userManager.GetUserFunctionsAsync(userId);
        }

        [HttpGet]
        public async Task<RequestResult> GetUserRoles(string userId)
        {
            return await _userManager.GetUserRolesAsync(userId);
        }
    }
}
