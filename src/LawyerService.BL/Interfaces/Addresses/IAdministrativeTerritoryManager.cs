using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Addresses
{
    public interface IAdministrativeTerritoryManager : IBaseManager<AdministrativeTerritory, AdministrativeTerritoryVM>
    {
        Task<List<AdministrativeTerritoryVM>> GetAllCurrentByCountryId(long countryId);
    }
}
