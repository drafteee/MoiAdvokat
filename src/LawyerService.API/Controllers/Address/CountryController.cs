using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;

namespace LawyerService.API.Controllers.Address
{
    public class CountryController : BaseController<ICountryManager, Country, CountryVM>
    {
        public CountryController(ICountryManager manager) : base(manager)
        {
        }
    }
}
