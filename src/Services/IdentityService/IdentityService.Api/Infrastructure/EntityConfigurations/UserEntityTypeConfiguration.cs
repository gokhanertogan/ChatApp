using IdentityService.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Api.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("user_hilo").IsRequired();

            builder.Property(ci => ci.DialingCode).IsRequired(true).HasMaxLength(4);

            builder.Property(ci => ci.MobilePhone).IsRequired(true).HasMaxLength(7);

            builder.Property(ci => ci.FirstName).IsRequired(true).HasMaxLength(20);

            builder.Property(ci => ci.LastName).IsRequired(true).HasMaxLength(20);

            builder.Property(ci => ci.Email).IsRequired(false).HasMaxLength(50);

            builder.Property(ci => ci.ProfileImageName).IsRequired(false).HasMaxLength(50);

            builder.Property(ci => ci.ExpireDate).IsRequired(false);

            builder.Property(s => s.IsActivation).IsRequired(true).HasDefaultValueSql<bool>("0");

            builder.Property(ci => ci.IsFirstLogin).IsRequired(false);

            builder.Property(ci => ci.IsMailActivation).IsRequired(false);

            builder.Property(ci => ci.LastPasswordChangeDate).IsRequired(false);
        }
    }
}
