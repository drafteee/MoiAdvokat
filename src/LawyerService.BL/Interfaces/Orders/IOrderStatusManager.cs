using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.BL.Interfaces.Orders
{
    public interface IOrderStatusManager : IBaseManager<OrderStatus, OrderStatusVM>
    {
    }
}
