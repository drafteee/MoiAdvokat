using LawyerService.BL.Interfaces.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
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
    }
}
