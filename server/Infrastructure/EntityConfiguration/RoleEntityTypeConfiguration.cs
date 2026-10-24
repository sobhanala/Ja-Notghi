using Domain.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = Role.Pro,
                NormalizedName = Role.Pro.ToUpper()
            });
        builder.HasData(
            new IdentityRole
            {
                Name = Role.Normal,
                NormalizedName = Role.Normal.ToUpper()
            });
    }
}