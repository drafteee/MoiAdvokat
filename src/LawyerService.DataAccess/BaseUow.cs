using LawyerService.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LawyerService.DataAccess.DataAccess
{
    public class BaseUow<T> : IBaseUow where T : DbContext
    {
        protected readonly T _context;
        private bool _disposed = false;

        protected BaseUow(T context)
        {
            this._context = context;
        }

        public Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransationAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                this._context?.Dispose();

            _disposed = true;
        }
    }
}
