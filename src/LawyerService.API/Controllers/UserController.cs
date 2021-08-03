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
        public async Task<RequestResult> Login(string password, string userName )
        {
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return await _userManager.LoginAsync(userName, password, ip, userAgent);
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
    }
}
