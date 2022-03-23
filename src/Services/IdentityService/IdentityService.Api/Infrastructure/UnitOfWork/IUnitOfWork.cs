using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complete();
    }
}
