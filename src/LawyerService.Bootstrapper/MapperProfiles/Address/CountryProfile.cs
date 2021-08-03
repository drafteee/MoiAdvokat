using AutoMapper;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.Bootstrapper.MapperProfiles.Address
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryVM>().ReverseMap();
        }
    }
}
