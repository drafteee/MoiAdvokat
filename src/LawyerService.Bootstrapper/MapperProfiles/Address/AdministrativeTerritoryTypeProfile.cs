using AutoMapper;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.Bootstrapper.MapperProfiles.Address
{
    public class AdministrativeTerritoryTypeProfile : Profile
    {
        public AdministrativeTerritoryTypeProfile()
        {
            CreateMap<AdministrativeTerritoryType, AdministrativeTerritoryTypeVM>().ReverseMap();
        }
    }
}
