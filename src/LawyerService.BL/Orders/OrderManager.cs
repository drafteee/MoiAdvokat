using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Orders;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Enums;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Errors;
using LawyerService.ViewModel.Files;
using LawyerService.ViewModel.Lawyers;
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

        public async Task<List<OrderVM>> GetOrders()
        {
            User user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
            var set = _uow.Set<Order>()
                .Include(x => x.User)
                .Select(x=> new OrderVM
                { 
                    NameClient = x.NameClient,
                    StartDate =  x.StartDate,
                    EndDueDate = x.EndDueDate,
                    Id = x.Id,
                    IsResponse = x.OrderResponses.Where(y => y.Lawyer.UserId == user.Id).FirstOrDefault() != null
                })
                .ToList();
            return _mapper.Map<List<OrderVM>>(set);
        }

        public async Task<bool> SubmitOrder(OrderVM orderVM)
        {
            var order = _mapper.Map<Order>(orderVM);
            order.StatusId = (await _uow.OrderStatuses.GetAsync(x => x.Code == ((int)OrderStatusEnum.Filed).ToString())).Select(x => x.Id).FirstOrDefault();
            order.CreatedOn = DateTime.Now;

            var orderSpecializations = orderVM.SpecializationsIds.ConvertAll(specializationId => new OrderSpecialization()
            {
                Order = order,
                SpecializationId = specializationId
            });

            _uow.OrderSpecializations.AddRange(orderSpecializations);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<OrderSubmitStarterInfoVM> GetStarterInfoForSubmit()
        {
            var specializations = _mapper.Map<List<SpecializationVM>>(await _uow.Specializations.GetAll());

            return new OrderSubmitStarterInfoVM()
            {
                Specializations = specializations
            };
        }

        public async Task<OrderVM> GetVMByIdAsync(long id, bool withDeleted = false) =>
            _mapper.Map<OrderVM>(await GetByIdAsync(id, withDeleted));

        public new async Task<Order> GetByIdAsync(long id, bool withDeleted = false) => 
            await _uow.Set<Order>().Where(x => x.Id == id && (withDeleted || !x.IsDeleted))
                .Include(x => x.OrderSpecializations)
                    .ThenInclude(x => x.Specialization).FirstOrDefaultAsync();

        public async Task<OrderVM> ExecuteOrder(AttachFileVM vm)
        {
            try
            {
                var orderFiles = vm.FilesIds.ConvertAll(fileId => new OrderFiles()
                {
                    OrderId = vm.EntityId,
                    FileId = fileId
                });

                Order order = await GetByIdAsync(vm.EntityId);
                order.FinishDate = DateTimeOffset.Now;

                _uow.Orders.Update(order);

                _uow.Set<OrderFiles>().AddRange(orderFiles);
                if (await _uow.SaveAsync() > 0)
                    return _mapper.Map<OrderVM>(order);

                throw new RestException(System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
