using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public class BaseManager<T, TVM> : IBaseManager<T, TVM>
        where T : BaseEntity
        where TVM : BaseVM
    {
        protected readonly IUow _uow;
        protected readonly IMapper _mapper;
        protected readonly IValidator<TVM> _validator;
        protected readonly ILocalizationManager _localizationManager;
        protected readonly IUserAccessor _userAccessor;
        protected readonly UserManager<User> _userManager;
        protected readonly IServiceProvider _serviceProvider;

        public BaseManager(IUow uow, IMapper mapper, IValidator<TVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
            _localizationManager = localizationManager;
            _userAccessor = userAccessor;
            _userManager = userManager;
            _serviceProvider = serviceProvider;
        }

        public async Task<List<TVM>> GetAllAsync(bool withDeleted = false) => _mapper.Map<List<TVM>>(await _uow.GetAll<T>(withDeleted));

        public async Task<TVM> GetByIdAsync(long id, bool withDeleted = false) => _mapper.Map<TVM>(await _uow.GetById<T>(id, withDeleted));

        public virtual async Task<bool> CreateOrUpdateAsync(TVM viewModel)
        {
            var entity = _mapper.Map<T>(viewModel);

            if (entity.Id != 0)
            {
                PopulateEntity(entity);
                _uow.Set<T>().Update(entity);
            }
            else
                _uow.Set<T>().Add(entity);

            return await _uow.SaveAsync() > 0;
        }

        public virtual async Task<bool> CreateOrUpdateManyAsync(List<TVM> viewModels)
        {
            var entities = _mapper.Map<List<T>>(viewModels);

            var toCreate = entities.Where(x => x.Id == 0).ToList();
            var toUpdate = entities.Where(x => x.Id != 0).ToList();

            PopulateEntities(toUpdate);

            _uow.Set<T>().AddRange(toCreate);
            _uow.Set<T>().UpdateRange(toUpdate);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            DbSet<T> set = _uow.Set<T>();

            T entity = await set.Where(x => x.Id == id).FirstOrDefaultAsync();

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            set.Update(entity);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> RestoreByIdAsync(long id)
        {
            DbSet<T> set = _uow.Set<T>();

            T entity = await set.Where(x => x.Id == id).FirstOrDefaultAsync();

            entity.IsDeleted = false;
            entity.DeletedOn = null;

            set.Update(entity);

            return await _uow.SaveAsync() > 0;
        }

        public async Task AddAsync(T entity)
        {
            await _uow.Set<T>().AddAsync(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _uow.SaveAsync() > 0;
        }

        protected void PopulateEntity(T entity)
        {
            var dbEntity = _uow.Set<T>().Where(t => t.Id == entity.Id && !t.IsDeleted).AsNoTracking().FirstOrDefault();

            entity.CreatedOn = dbEntity.CreatedOn;
            entity.IsDeleted = dbEntity.IsDeleted;
            entity.DeletedOn = dbEntity.DeletedOn;
        }

        protected void PopulateEntities(List<T> entities)
        {
            var dbEntities = _uow.Set<T>().AsNoTracking().AsEnumerable().Where(t => entities.Any(e => e.Id == t.Id) && !t.IsDeleted).ToList();

            entities.ForEach(e =>
            {
                var dbEntity = dbEntities.Where(t => t.Id == e.Id).FirstOrDefault();

                e.CreatedOn = dbEntity.CreatedOn;
                e.IsDeleted = dbEntity.IsDeleted;
                e.DeletedOn = dbEntity.DeletedOn;
            });
        }
    }
}
