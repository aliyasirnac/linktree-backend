using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AppRepository : EfRepositoryBase<App, int, BaseDbContext>, IAppRepository
{
    public AppRepository(BaseDbContext context) : base(context)
    {
    }
}