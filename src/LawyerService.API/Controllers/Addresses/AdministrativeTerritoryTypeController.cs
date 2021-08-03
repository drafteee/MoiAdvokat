using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.API.Controllers.Addresses
{
    public class AdministrativeTerritoryTypeController : BaseController<IAdministrativeTerritoryTypeManager, AdministrativeTerritoryType, AdministrativeTerritoryTypeVM>
    {
        public AdministrativeTerritoryTypeController(IAdministrativeTerritoryTypeManager manager) : base(manager)
        {
        }
    }
}
