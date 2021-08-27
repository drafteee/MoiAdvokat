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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL.Orders
{
    public class OrderResponseManager : BaseManager<OrderResponse, OrderResponseVM>, IOrderResponseManager
    {
        public OrderResponseManager(IUow uow, IMapper mapper, IValidator<OrderResponseVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider)
            : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }
    }
}
