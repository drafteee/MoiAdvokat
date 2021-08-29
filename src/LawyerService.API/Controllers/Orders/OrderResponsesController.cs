using LawyerService.BL.Interfaces.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Orders
{
    [Route("api/[controller]/[action]")]
    public class OrderResponsesController : BaseController<IOrderResponseManager, OrderResponse, OrderResponseVM>
    {
        public OrderResponsesController(IOrderResponseManager manager) : base(manager)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> RespondOrder([FromBody] OrderResponseVM order)
        {
            try
            {
                return await _manager.RespondOrder(order);

            }catch(Exception e)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<OrderResponseVM>> GetResponses([FromHeader] OrderResponseVM orderVM)
        {
            return await _manager.GetResponses(orderVM);
        }
        
    }
}
