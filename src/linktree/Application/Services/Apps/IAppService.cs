using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Apps;

public interface IAppService
{
    Task<App?> GetAsync(
        Expression<Func<App, bool>> predicate,
        Func<IQueryable<App>, IIncludableQueryable<App, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<App>?> GetListAsync(
        Expression<Func<App, bool>>? predicate = null,
        Func<IQueryable<App>, IOrderedQueryable<App>>? orderBy = null,
        Func<IQueryable<App>, IIncludableQueryable<App, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<App> AddAsync(App app);
    Task<App> UpdateAsync(App app);
    Task<App> DeleteAsync(App app, bool permanent = false);
}
