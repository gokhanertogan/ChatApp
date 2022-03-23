using IdentityService.Api.Infrastructure.Context;
using IdentityService.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IdentityContext context) : base(context)
        {

        }

        public async Task AddAsync(User user)
        {
            user.Id = _context.Users.Max(x => x.Id) + 1; 
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
                _context.Users.Remove(user);
        }

        public void Update(User user, int userId)
        {
            user.Id = userId;
            _context.Users.Update(user);
        }

        public async Task<User> UserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id==userId);
        }

        public async Task<User> GetByPhoneNumberAsync(string dialingCode, string mobilePhone)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.DialingCode==dialingCode && x.MobilePhone==mobilePhone);
        }
    }
}
