using LawyerService.BL.Interfaces.Addresses;
using LawyerService.Entities.Address;
using LawyerService.ViewModel.Address;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Addresses
{
    public class AddressController : BaseController<IAddressManager, Address, AddressVM>
    {
        public AddressController(IAddressManager manager) : base(manager)
        {
        }
    }
}
