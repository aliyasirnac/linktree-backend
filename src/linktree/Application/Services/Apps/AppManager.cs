using Application.Features.Apps.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Apps;

public class AppManager : IAppService
{
    private readonly IAppRepository _appRepository;
    private readonly AppBusinessRules _appBusinessRules;

    public AppManager(IAppRepository appRepository, AppBusinessRules appBusinessRules)
    {
        _appRepository = appRepository;
        _appBusinessRules = appBusinessRules;
    }

    public async Task<App?> GetAsync(
        Expression<Func<App, bool>> predicate,
        Func<IQueryable<App>, IIncludableQueryable<App, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        App? app = await _appRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return app;
    }

    public async Task<IPaginate<App>?> GetListAsync(
        Expression<Func<App, bool>>? predicate = null,
        Func<IQueryable<App>, IOrderedQueryable<App>>? orderBy = null,
        Func<IQueryable<App>, IIncludableQueryable<App, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<App> appList = await _appRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return appList;
    }

    public async Task<App> AddAsync(App app)
    {
        App addedApp = await _appRepository.AddAsync(app);

        return addedApp;
    }

    public async Task<App> UpdateAsync(App app)
    {
        App updatedApp = await _appRepository.UpdateAsync(app);

        return updatedApp;
    }

    public async Task<App> DeleteAsync(App app, bool permanent = false)
    {
        App deletedApp = await _appRepository.DeleteAsync(app);

        return deletedApp;
    }
}
