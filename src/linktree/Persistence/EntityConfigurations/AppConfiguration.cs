using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppConfiguration : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        builder.ToTable("Apps").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.Name).HasColumnName("Name");
        builder.Property(a => a.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(a => a.PlayStoreUrl).HasColumnName("PlayStoreUrl");
        builder.Property(a => a.AppStoreUrl).HasColumnName("AppStoreUrl");
        builder.Property(a => a.CompanyId).HasColumnName("CompanyId");
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.Company);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}
