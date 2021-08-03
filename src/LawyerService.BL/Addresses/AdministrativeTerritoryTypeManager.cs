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

namespace LawyerService.BL.Addresses
{
    public class AdministrativeTerritoryTypeManager : BaseManager<AdministrativeTerritoryType, AdministrativeTerritoryTypeVM>, IAdministrativeTerritoryTypeManager
    {
        public AdministrativeTerritoryTypeManager(IUow uow, IMapper mapper, IValidator<AdministrativeTerritoryTypeVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider) : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }
    }
}
