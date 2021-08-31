using AutoMapper;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using System.Linq;

namespace LawyerService.Bootstrapper.MapperProfiles.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVM>().AfterMap((src, dest) =>
            {
                dest.SpecializationsStr = string.Join(", ", src.OrderSpecializations.Select(x => x.Specialization.Name));
            });
            CreateMap<OrderVM, Order>();
        }
    }
}
