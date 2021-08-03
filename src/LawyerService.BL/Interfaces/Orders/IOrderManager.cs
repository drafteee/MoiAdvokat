using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.BL.Interfaces.Orders
{
    public interface IOrderManager : IBaseManager<Order, OrderVM>
    {
    }
}
