using AutoMapper;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;

namespace LawyerService.Bootstrapper.MapperProfiles.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVM>().ReverseMap();
        }
    }
}
