using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces
{
    public interface ICRUD<TEntity>
         where TEntity : class, new()
    {
        abstract void Add(TEntity entity);
        abstract Task<bool> SaveChanges();
        abstract bool Create(TEntity entity);
        abstract TEntity Read(long id);
        abstract Task<(long, int, int, long, List<TEntity>)> ReadList(int page, int pageSize);
        abstract Task<List<TEntity>> ReadAll();
        abstract TEntity Update(TEntity entity, long id, List<Expression<Func<TEntity, object>>> properties = null, bool needSave = false);
        abstract Task<int> Delete(long id);
        abstract IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> func);
    }
}
