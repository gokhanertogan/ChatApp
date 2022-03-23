using IdentityService.Api.Infrastructure.EntityConfigurations;
using IdentityService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api.Infrastructure.Context
{
    public class IdentityContext: DbContext
    {
        //public const string DEFAULT_SCHEME = "identity";

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
