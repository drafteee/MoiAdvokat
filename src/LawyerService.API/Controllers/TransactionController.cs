using LawyerService.BL.Interfaces.Transactions;
using LawyerService.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController: ControllerBase
    {
        private readonly ITransactionManager _transactionManager;

        public TransactionController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<RequestResult> CreateTransactionInService(decimal amount)
        {
            return await _transactionManager.CreateTransactionInService(amount);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<RequestResult> CreateTransactionOutService(decimal amount)
        {
            return await _transactionManager.CreateTransactionOutService(amount);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<RequestResult> CreateRole(bool isSuccess, long transactionId)
        {
            return await _transactionManager.GetResultOfTransaction(isSuccess, transactionId);
        }
    }
}
