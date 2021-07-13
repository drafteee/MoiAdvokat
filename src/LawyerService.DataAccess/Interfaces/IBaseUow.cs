using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LawyerService.DataAccess.Interfaces
{
    public interface IBaseUow : IDisposable
    {
        Task<IDbContextTransaction> BeginTransationAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
