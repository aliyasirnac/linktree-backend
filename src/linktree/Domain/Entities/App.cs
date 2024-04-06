using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class App : Entity<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? PlayStoreUrl { get; set; }
    public string? AppStoreUrl { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
