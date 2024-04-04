using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Company:Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public virtual ICollection<App> Apps { get; set; }
}