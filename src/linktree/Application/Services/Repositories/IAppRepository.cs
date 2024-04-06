using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppRepository : IAsyncRepository<App, int>, IRepository<App, int> { }
