using AutoMapper;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.Bootstrapper.MapperProfiles.Orders
{
    public class OrderStatusProfile : Profile
    {
        public OrderStatusProfile()
        {
            CreateMap<OrderStatus, OrderStatusVM>().ReverseMap();
        }
    }
}
