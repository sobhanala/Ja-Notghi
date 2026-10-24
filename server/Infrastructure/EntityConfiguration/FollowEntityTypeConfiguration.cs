using Domain.Constant;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class FollowEntityTypeConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.HasKey(f => new { f.FollowerUserName, f.FollowedUserName });
        
        builder.Property(f => f.FollowerUserName).IsRequired();
        builder.Property(f => f.FollowedUserName).IsRequired();
        builder.Property(f => f.FollowedAt).IsRequired();
        builder.Property(f => f.FollowedUserName).HasMaxLength(IdentityRules.UsernameMaxLength);
        builder.Property(f => f.FollowerUserName).HasMaxLength(IdentityRules.UsernameMaxLength);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.FollowerUserName)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.FollowedUserName)
            .OnDelete(DeleteBehavior.Restrict);
    }
}