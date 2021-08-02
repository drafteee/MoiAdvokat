using AutoMapper;
using LawyerService.ViewModel.Address;

namespace LawyerService.Bootstrapper.MapperProfiles.Address
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Entities.Address.Address, AddressVM>().ReverseMap();
        }
    }
}
