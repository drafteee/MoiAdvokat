using LawyerService.Entities.Order;
using LawyerService.ViewModel.Files;
using LawyerService.ViewModel.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Orders
{
    public interface IOrderManager : IBaseManager<Order, OrderVM>
    {
        Task<List<Order>> GetOrders();
        Task<bool> SubmitOrder(OrderVM order);
        Task<OrderSubmitStarterInfoVM> GetStarterInfoForSubmit();
        Task<bool> ExecuteOrder(AttachFileVM vm);
    }
}
