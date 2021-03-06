using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Addresses;
using LawyerService.BL.Interfaces.Lawyers;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Errors;
using LawyerService.ViewModel.Files;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Lawyers
{
    public class LawyerManager : BaseManager<Lawyer, LawyerVM>, ILawyerManager
    {
        private readonly IConfiguration _configuration;

        public LawyerManager(IUow uow,
            IMapper mapper,
            IValidator<LawyerVM> validator,
            ILocalizationManager localisationManager,
            IUserAccessor userAccessor,
            UserManager<User> userManager,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
            : base(uow, mapper, validator, localisationManager, userAccessor, userManager, serviceProvider)
        {
            _configuration = configuration;
        }

        public async Task<bool> CheckIfCertificateExists(LawyerVM vm)
        {
            return (await _uow.Set<Lawyer>().Where(x => x.Id == vm.Id).Select(x => x.FileCopyId).FirstOrDefaultAsync()).HasValue;
        }

        public async Task<bool> CheckIfLawyerCanBeConfirmed(long id)
        {
            var lawyer = await _uow.Set<Lawyer>().Where(x => x.Id == id).FirstOrDefaultAsync();

            return lawyer.FileCopyId.HasValue && !lawyer.IsVerified.HasValue;
        }

        public async Task<bool> ConfirmLawyer(LawyerConfirmationVM vm)
        {
            var lawyer = await _uow.Set<Lawyer>().Where(x => x.Id == vm.Id)
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            lawyer.IsVerified = vm.IsVerified;

            if (!vm.IsVerified)
                lawyer.FileCopyId = null;

            _uow.Lawyer.Update(lawyer);
            var result = await _uow.SaveAsync() > 0;

            //string mailBody = vm.IsVerified
            //    ? "Ваша заявка была рассмотрена и подтверждена."
            //    : "Ваша заявка была рассмотрена модераторами. К сожалению, ваша заявка не была принята."; // TODO добавить перевод на казахский

            //await MailService.SendAsync(_configuration, mailBody, "Рассмотрение заявки на \"Мой адвокат\"", lawyer.User.Email);

            return result;
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
            try
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
            catch (Exception e)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { e.Message, e.InnerException });
            }
        }

        public override async Task<LawyerVM> GetByIdAsync(long id, bool withDeleted = false)
        {
            var lawyer = await _uow.Set<Lawyer>().Where(x => x.Id == id && (withDeleted || !x.IsDeleted))
                .Include(x => x.FileCopy)
                .Select(x => new Lawyer()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    DateOfIssue = x.DateOfIssue,
                    DeletedOn = x.DeletedOn,
                    FileCopyId = x.FileCopyId,
                    IsDeleted = x.IsDeleted,
                    IsVerified = x.IsVerified,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    LicenseNumber = x.LicenseNumber,
                    FileCopy = new Entities.File()
                    {
                        FileName = x.FileCopy.FileName
                    }
                }).FirstOrDefaultAsync();

            return _mapper.Map<LawyerVM>(lawyer);
        }

        public async Task<bool> UploadCertificate(AttachFileVM vm)
        {
            var lawyer = _uow.Lawyer.GetById(vm.EntityId);

            if (lawyer.FileCopyId.HasValue)
                throw new RestException(System.Net.HttpStatusCode.BadRequest); // TODO сообщение о том, что сертификат уже загружен

            lawyer.FileCopyId = vm.FilesIds[0];
            lawyer.IsVerified = null;

            _uow.Lawyer.Update(lawyer);
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
                    //&& a.Address.CountryId == x.CountryId
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
