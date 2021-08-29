﻿using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Orders
{
    public interface IOrderManager : IBaseManager<Order, OrderVM>
    {
        Task<List<OrderVM>> GetOrders();
        Task<bool> SubmitOrder(OrderVM order);
        Task<OrderSubmitStarterInfoVM> GetStarterInfoForSubmit();
    }
}
