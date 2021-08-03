using LawyerService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LawyerService.BL.Entity;
using LawyerService.BL.Interfaces;

namespace LawyerService.BL.Service
{
    public class CRUDService<TEntity> : BaseService, ICRUD<TEntity>
            where TEntity : class, new()
    {
        public CRUDService()
        {

        }

        public CRUDService(LawyerDbContext db) : base(db)
        {

        }

        protected IQueryable<TEntity> GetEntities(ref int page, ref int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, string orderDirection = null)
        {
            var dbSet = _db.Set<TEntity>();

            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var dbWhere = where == null ? dbSet : dbSet.Where(where);

            var dbOrder = orderDirection == null ? dbWhere : orderDirection == "descend" ? dbWhere.OrderByDescending(order) : dbWhere.OrderBy(order);

            return dbOrder.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Add(TEntity entity)
        {
            try
            {
                _db.Set<TEntity>().Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(TEntity entity)
        {
            try
            {
                _db.Set<TEntity>().Add(entity);

                return _db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(List<TEntity> entities)
        {
            try
            {
                _db.Set<TEntity>().AddRange(entities);

                return _db.SaveChanges() == entities.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(long id)
        {
            var entity = _db.Set<TEntity>().Find(id);

            if (entity == null)
            {
                throw new Exception("Записи не существует.");
            }

            _db.Set<TEntity>().Remove(entity);

            return await _db.SaveChangesAsync();
        }

        public TEntity Read(long id)
        {
            var entity = _db.Set<TEntity>().Find(id);

            if (entity == null)
            {
                throw new Exception("Записи не существует.");
            }

            return entity;
        }

        public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> func)
        {
            return _db.Set<TEntity>().Where(func);
        }

        public async Task<(long, int, int, long, List<TEntity>)> ReadList(int page, int pageSize)
        {
            var query = _db.Set<TEntity>();

            if (page == 0)
            {
                page = 1;
            }

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (count, page, pageSize, count / pageSize + ((count % pageSize > 0) ? 1 : 0), items);
        }

        public async Task<List<TEntity>> ReadAll() => await _db.Set<TEntity>().ToListAsync();

        public async Task<bool> SaveChanges()
        {
            try
            {
                return (await _db.SaveChangesAsync() > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Update(TEntity entity, long id, List<Expression<Func<TEntity, object>>> properties = null, bool needSave = false)
        {
            TEntity entityBd = null;
            int countUpdateFields = 0;

            try
            {
                entityBd = Read(id);
                var dbEntry = _db.Entry(entityBd);
                var entityType = typeof(TEntity);
                IEnumerable<PropertyInfo> propertyInfos;

                var navs = EntityManager.GetDeclaredPropertiesForType(_db, entityBd.GetType());

                if (properties == null)
                    propertyInfos = entityType.GetProperties().Where(p => !p.PropertyType.IsAbstract && !navs.Contains(p.PropertyType.Name) &&
                                                                          p.GetCustomAttribute<KeyAttribute>() == null &&
                                                                          p.GetCustomAttribute<DatabaseGeneratedAttribute>() == null);
                else
                    propertyInfos = properties.Select(p => entityType.GetProperty((p.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)p.Body).Operand).Member.Name));

                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyName = propertyInfo.Name;
                    var value = propertyInfo.GetValue(entity, null);
                    var valueBd = propertyInfo.GetValue(entityBd, null);

                    if ((value != null && !value.Equals(valueBd)) || (valueBd != null && !valueBd.Equals(value)))
                    {
                        dbEntry.Property(propertyInfo.Name).IsModified = true;
                        propertyInfo.SetValue(entityBd, value);
                        countUpdateFields++;
                    }
                }

                if (countUpdateFields > 0 && needSave)
                {
                    var result = _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entityBd;
        }
    }
}
