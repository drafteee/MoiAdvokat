using LawyerService.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        T GetById(object id);
        IQueryable<T> GetQueryable();
        Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IQueryable<T> entities);
        IQueryable<T> FromQueryRaw(string query, params object[] @params);
        Task<int> ExecuteQueryRawAsync(string query, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
    }
}
