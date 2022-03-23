using IdentityService.Api.Infrastructure.Context;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IdentityContext _context;

        public UnitOfWork(IdentityContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
