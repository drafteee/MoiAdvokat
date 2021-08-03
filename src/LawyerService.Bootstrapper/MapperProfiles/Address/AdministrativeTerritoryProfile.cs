using AutoMapper;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.Bootstrapper.MapperProfiles.Address
{
    public class AdministrativeTerritoryProfile : Profile
    {
        public AdministrativeTerritoryProfile()
        {
            CreateMap<AdministrativeTerritory, AdministrativeTerritoryVM>().ReverseMap();
        }
    }
}
