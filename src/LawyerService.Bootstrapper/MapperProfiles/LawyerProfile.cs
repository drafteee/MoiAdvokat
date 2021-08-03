using AutoMapper;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class LawyerProfile : Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerVM>();
            CreateMap<LawyerVM, Lawyer>().AfterMap((src, dest) =>
            {
                dest.Address.CountryId = src.Address.Country.Id;
                dest.Address.Country = null;

                dest.Address.AdministrativeTerritoryId = src.Address.AdministrativeTerritory.Id;
                dest.Address.AdministrativeTerritory = null;
            });
        }
    }
}
