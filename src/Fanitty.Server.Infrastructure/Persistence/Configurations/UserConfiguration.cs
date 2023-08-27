using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fanitty.Server.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Uuid).IsUnique();
        builder.Property(x => x.Uuid).IsRequired();

        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Username).HasMaxLength(UserSettings.UsernameMaxLength);

        builder.Property(x => x.DisplayName).HasMaxLength(UserSettings.DisplayNameMaxLength);
        builder.Property(x => x.DisplayName).IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.WithOwner();

            email.Property(x => x.Value).IsRequired();
        }).Navigation(x => x.Email).IsRequired();
    }
}
