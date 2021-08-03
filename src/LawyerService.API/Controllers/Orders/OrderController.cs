using LawyerService.BL.Interfaces.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.API.Controllers.Orders
{
    public class OrderController : BaseController<IOrderManager, Order, OrderVM>
    {
        public OrderController(IOrderManager manager) : base(manager)
        {
        }
    }
}
