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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Orders
{
    public class OrderManager : BaseManager<Order, OrderVM>, IOrderManager
    {
        public OrderManager(IUow uow, IMapper mapper, IValidator<OrderVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider) 
            : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }

        public async Task<List<Order>> GetOrders()
        {
            var set = _uow.Orders.GetQueryable().Include(x => x.User).ToList();

            return set;
        }
    }
}
