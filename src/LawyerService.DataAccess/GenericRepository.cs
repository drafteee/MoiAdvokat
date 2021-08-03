using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LawyerService.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dataSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return _dataSet.Find(id);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _dataSet.AsQueryable<T>();
        }

        public virtual async Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dataSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }

        public virtual void Add(T entity)
        {
            _dataSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dataSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dataSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dataSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            _dataSet.Remove(entity);
        }

        public virtual void DeleteRange(IQueryable<T> entities)
        {
            _dataSet.RemoveRange(entities);
        }

        public virtual IQueryable<T> FromQueryRaw(string query, params object[] parameters)
        {
            return _dataSet.FromSqlRaw(query, parameters);
        }

        public virtual Task<int> ExecuteQueryRawAsync(string query, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return _context.Database.ExecuteSqlRawAsync(query, parameters, cancellationToken);
        }
    }
}
