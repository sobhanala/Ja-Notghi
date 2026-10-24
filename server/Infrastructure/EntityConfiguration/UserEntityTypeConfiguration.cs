using Domain.Constant;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.UserName);
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.FirstName).IsRequired();
        builder.Property(user => user.LastName).IsRequired();
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.UserName).IsRequired();
        builder.Property(user => user.PasswordHash).IsRequired();
        builder.Property(user => user.Gender).IsRequired();
        builder.Property(user => user.DateOfBirth).IsRequired();
        builder.Property(user => user.FollowersCount).HasDefaultValue(0);
        builder.Property(user => user.FollowingsCount).HasDefaultValue(0);

        builder.Property(user => user.FirstName).HasMaxLength(IdentityRules.FirstNameMaxLength);
        builder.Property(user => user.LastName).HasMaxLength(IdentityRules.LastNameMaxLength);
        builder.Property(user => user.UserName).HasMaxLength(IdentityRules.UsernameMaxLength);
        builder.Property(user => user.Email).HasMaxLength(IdentityRules.EmailMaxLength);
    }
}