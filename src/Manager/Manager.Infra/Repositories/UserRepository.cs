using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;

        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAsNoTrackingAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Set<User>()
                                .AsNoTracking()
                                .Where(expression)
                                .ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await this.GetAsNoTrackingAsync(x => x.Email.ToLower() == email.ToLower());

            return users.FirstOrDefault();
        }

        public async Task<ICollection<User>> SearchByEmailAsync(string email)
        {
            var users = await this.GetAsNoTrackingAsync(x => x.Email.ToLower().Contains(email.ToLower()));

            return users;
        }

        public async Task<ICollection<User>> SearchByNameAsync(string name)
        {
            var users = await this.GetAsNoTrackingAsync(x => x.Name.ToLower().Contains(name.ToLower()));

            return users;
        }
    }
}
