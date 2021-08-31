using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Orders;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Errors;
using LawyerService.ViewModel.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ChooseLawyer(OrderResponseVM orderResponseVM)
        {
            var orderResponse = (await _uow.OrderResponses.GetAsync(x => x.Id == orderResponseVM.Id)).FirstOrDefault();
            var order = (await _uow.Orders.GetAsync(x => x.Id == orderResponseVM.OrderId)).FirstOrDefault();
            User user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

            orderResponse.IsChoosen = true;
            order.LawyerId = orderResponseVM.LawyerId;
            try
            {
                _uow.OrderResponses.Update(orderResponse);
                _uow.Orders.Update(order);

                return await _uow.SaveAsync() > 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<OrderResponseVM>> GetResponses(OrderResponseVM orderResponse)
        {
            var responses = _uow.Set<OrderResponse>()
                .Include(x=> x.Lawyer)
                .Where(x => x.OrderId == orderResponse.OrderId).ToList();
            return _mapper.Map<List<OrderResponseVM>>(responses);
        }

        public async Task<bool> RespondOrder(OrderResponseVM orderResponseVM)
        {
            var orderResponse = _mapper.Map<OrderResponse>(orderResponseVM);
            User user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

            var lawyer = (await _uow.Lawyer.GetAsync(x => x.UserId == user.Id)).FirstOrDefault();

            if(lawyer != null)
            {
                orderResponse.LawyerId = (await _uow.Lawyer.GetAsync(x => x.UserId == user.Id)).FirstOrDefault().Id;
            }
            else
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Пользователь не подтвержден в качестве адвоката!");
            }


            _uow.OrderResponses.Add(orderResponse);

            return await _uow.SaveAsync() > 0;
        }
    }
}
