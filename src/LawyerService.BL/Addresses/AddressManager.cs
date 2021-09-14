using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Addresses;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Address;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel.Address;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawyerService.BL.Addresses
{
    public class AddressManager : BaseManager<Address, AddressVM>, IAddressManager
    {
        public AddressManager(IUow uow, IMapper mapper, IValidator<AddressVM> validator, ILocalizationManager localisationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider)
            : base(uow, mapper, validator, localisationManager, userAccessor, userManager, serviceProvider)
        {
        }

        public List<Address> GetExistingAddresses(List<Address> addresses)
        {
            var set = _uow.Set<Address>();

            List<Address> existingAddresses = set.AsEnumerable().Where(x => addresses.Any(a => !a.IsDeleted
                && a.AdministrativeTerritoryId == x.AdministrativeTerritoryId
                //&& a.CountryId == x.CountryId
                && a.Street == x.Street
                && a.House == x.House
                && a.Office == x.Office)).ToList();

            var newAddresses = new List<Address>(addresses);
            newAddresses.ForEach(a =>
            {
                a.Id = existingAddresses.Where(x => a.AdministrativeTerritoryId == x.AdministrativeTerritoryId
                    //&& a.CountryId == x.CountryId
                    && a.Street == x.Street
                    && a.House == x.House
                    && a.Office == x.Office).Select(x => x.Id).FirstOrDefault();
            });

            return newAddresses;
        }
    }
}
