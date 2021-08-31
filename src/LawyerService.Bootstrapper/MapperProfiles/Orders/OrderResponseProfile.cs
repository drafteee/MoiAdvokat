using AutoMapper;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.Bootstrapper.MapperProfiles.Orders
{
    public class OrderResponseProfile : Profile
    {
        public OrderResponseProfile()
        {
            CreateMap<OrderResponse, OrderResponseVM>().ReverseMap();
        }
    }
}
