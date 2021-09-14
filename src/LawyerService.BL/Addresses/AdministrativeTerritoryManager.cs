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
using System.Threading.Tasks;

namespace LawyerService.BL.Addresses
{
    public class AdministrativeTerritoryManager : BaseManager<AdministrativeTerritory, AdministrativeTerritoryVM>, IAdministrativeTerritoryManager
    {
        public AdministrativeTerritoryManager(IUow uow, IMapper mapper, IValidator<AdministrativeTerritoryVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider) : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }

        public async Task<List<AdministrativeTerritoryVM>> GetAllCurrentByCountryId(long countryId) => 
            _mapper.Map<List<AdministrativeTerritoryVM>>(await _uow.AdministrativeTerritory.GetAsync(at => at.CountryId == countryId));
    }
}
