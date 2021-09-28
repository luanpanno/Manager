using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;

        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Set<User>()
                                .AsNoTracking()
                                .Where(x => x.Email.ToLower() == email.ToLower())
                                .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<List<User>> SearchByEmailAsync(string email)
        {
            var user = await _context.Set<User>()
                    .AsNoTracking()
                    .Where(x => x.Email.ToLower().Contains(email.ToLower()))
                    .ToListAsync();

            return user;
        }

        public async Task<List<User>> SearchByNameAsync(string name)
        {
            var user = await _context.Set<User>()
                                .AsNoTracking()
                                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                                .ToListAsync();

            return user;
        }
    }
}
