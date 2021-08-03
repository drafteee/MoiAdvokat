using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.API.Controllers.Addresses
{
    public class AdministrativeTerritoryController : BaseController<IAdministrativeTerritoryManager, AdministrativeTerritory, AdministrativeTerritoryVM>
    {
        public AdministrativeTerritoryController(IAdministrativeTerritoryManager manager) : base(manager)
        {
        }
    }
}
