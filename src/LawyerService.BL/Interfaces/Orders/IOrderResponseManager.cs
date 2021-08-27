using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Orders
{
    public interface IOrderResponseManager : IBaseManager<OrderResponse, OrderResponseVM>
    {
    }
}
