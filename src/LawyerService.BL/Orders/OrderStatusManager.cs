using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Orders;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using Microsoft.AspNetCore.Identity;
using System;

namespace LawyerService.BL.Orders
{
    public class OrderStatusManager : BaseManager<OrderStatus, OrderStatusVM>, IOrderStatusManager
    {
        public OrderStatusManager(IUow uow, IMapper mapper, IValidator<OrderStatusVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider) : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }
    }
}
