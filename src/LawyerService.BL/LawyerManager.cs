using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Addresses;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public class LawyerManager : BaseManager<Lawyer, LawyerVM>, ILawyerManager
    {
        public LawyerManager(IUow uow, IMapper mapper, IValidator<LawyerVM> validator, ILocalizationManager localisationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider)
            : base(uow, mapper, validator, localisationManager, userAccessor, userManager, serviceProvider)
        {
        }

        public override async Task<bool> CreateOrUpdateAsync(LawyerVM lawyerVM)
        {
            var lawyer = _mapper.Map<Lawyer>(lawyerVM);
            IdentifyAddress(lawyer);

            if (lawyer.Id != 0)
            {
                PopulateEntity(lawyer);
                _uow.Lawyer.Update(lawyer);
            }
            else
                _uow.Lawyer.Add(lawyer);

            return await _uow.SaveAsync() > 0;
        }

        public override async Task<bool> CreateOrUpdateManyAsync(List<LawyerVM> lawyerVMs)
        {
            var lawyers = _mapper.Map<List<Lawyer>>(lawyerVMs);
            IdentifyAddress(lawyers);

            var toCreate = lawyers.Where(x => x.Id == 0).ToList();
            var toUpdate = lawyers.Where(x => x.Id != 0).ToList();

            PopulateEntities(toUpdate);

            _uow.Lawyer.AddRange(toCreate);
            _uow.Lawyer.UpdateRange(toUpdate);

            return await _uow.SaveAsync() > 0;
        }

        #region Private methods

        private void IdentifyAddress(Lawyer lawyer) => IdentifyAddress(new List<Lawyer>() { lawyer });

        private void IdentifyAddress(List<Lawyer> lawyers)
        {
            var addressManager = _serviceProvider.GetRequiredService<IAddressManager>();
            var newAddresses = addressManager.GetExistingAddresses(lawyers.Select(x => x.Address).ToList());

            lawyers.ForEach(a =>
            {
                var address = newAddresses.Where(x => a.Address.AdministrativeTerritoryId == x.AdministrativeTerritoryId
                    && a.Address.CountryId == x.CountryId
                    && a.Address.Street == x.Street
                    && a.Address.House == x.House
                    && a.Address.Office == x.Office).FirstOrDefault();

                if (address.Id != 0)
                {
                    a.Address = null;
                    a.AddressId = address.Id;
                }
            });
        }

        #endregion
    }
}
