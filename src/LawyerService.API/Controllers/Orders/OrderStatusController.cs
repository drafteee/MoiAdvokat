using LawyerService.BL.Interfaces.Orders;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.API.Controllers.Orders
{
    public class OrderStatusController : BaseController<IOrderStatusManager, OrderStatus, OrderStatusVM>
    {
        public OrderStatusController(IOrderStatusManager manager) : base(manager)
        {
        }
    }
}
