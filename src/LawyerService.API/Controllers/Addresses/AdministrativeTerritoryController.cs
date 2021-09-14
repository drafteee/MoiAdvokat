using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Addresses
{
    public class AdministrativeTerritoryController : BaseController<IAdministrativeTerritoryManager, AdministrativeTerritory, AdministrativeTerritoryVM>
    {
        public AdministrativeTerritoryController(IAdministrativeTerritoryManager manager) : base(manager)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<List<AdministrativeTerritoryVM>> GetAllCurrentByCountryId([FromHeader] AdministrativeTerritoryVM vm)
        {
            return _manager.GetAllCurrentByCountryId(vm.CountryId);
        }
    }
}
