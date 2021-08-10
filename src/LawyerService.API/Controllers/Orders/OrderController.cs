using LawyerService.BL.Interfaces.Orders;
using LawyerService.BL.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Orders
{
    [Route("api/[controller]/[action]")]
    public class OrderController : BaseController<IOrderManager, Order, OrderVM>
    {

        public OrderController(IOrderManager manager) : base(manager)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Order>> GetOrders()
        {
            return await _manager.GetOrders();
        }
    }
}
