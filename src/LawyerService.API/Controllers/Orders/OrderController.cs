using LawyerService.BL.Interfaces.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Base;
using LawyerService.ViewModel.Files;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> SubmitOrder([FromBody] OrderVM order)
        {
            return await _manager.SubmitOrder(order);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<OrderSubmitStarterInfoVM> GetStarterInfoForSubmit()
        {
            return await _manager.GetStarterInfoForSubmit();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<OrderVM> GetOne([FromHeader] BaseVM order)
        {
            return await _manager.GetByIdAsync(order.Id);
        }

        public async Task<bool> Execute([FromBody] AttachFileVM vm)
        {
            return await _manager.ExecuteOrder(vm);
        }
    }
}
