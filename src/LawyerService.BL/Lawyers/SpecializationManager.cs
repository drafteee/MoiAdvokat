using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Lawyers;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Identity;
using System;

namespace LawyerService.BL.Lawyers
{
    public class SpecializationManager : BaseManager<Specialization, SpecializationVM>, ISpecializationManager
    {
        public SpecializationManager(IUow uow, IMapper mapper, IValidator<SpecializationVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider) : base(uow, mapper, validator, localizationManager, userAccessor, userManager, serviceProvider)
        {
        }
    }
}
