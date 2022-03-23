
using IdentityService.Api.Infrastructure.Context;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly IdentityContext _context;

        public BaseRepository(IdentityContext context)
        {
            _context = context;
        }
    }
}
