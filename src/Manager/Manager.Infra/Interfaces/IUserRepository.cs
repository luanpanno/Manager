using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<ICollection<User>> SearchByNameAsync(string name);
        Task<ICollection<User>> SearchByEmailAsync(string email);
        Task<ICollection<User>> GetAsNoTrackingAsync(Expression<Func<User, bool>> expression);
    }
}
