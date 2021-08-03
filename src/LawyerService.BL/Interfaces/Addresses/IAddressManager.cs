using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;
using System.Collections.Generic;

namespace LawyerService.BL.Interfaces.Addresses
{
    public interface IAddressManager : IBaseManager<Address, AddressVM>
    {
        List<Address> GetExistingAddresses(List<Address> addresses);
    }
}
