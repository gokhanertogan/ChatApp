using IdentityService.Api.Models;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        void Update(User user, int userId);
        Task DeleteAsync(int userId);
        Task<User> UserByIdAsync(int userId);
        Task<User> GetByPhoneNumberAsync(string dialingCode, string mobilePhone);
    }
}
