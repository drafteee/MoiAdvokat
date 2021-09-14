using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Addresses
{
    [Route("api/[controller]/[action]")]
    public class CountryController : BaseController<ICountryManager, Country, CountryVM>
    {
        public CountryController(ICountryManager manager) : base(manager)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public override Task<List<CountryVM>> GetAllCurrent()
        {
            return _manager.GetAllAsync();
        }
    }
}
