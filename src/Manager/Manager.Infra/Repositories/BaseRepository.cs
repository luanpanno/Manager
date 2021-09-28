using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext _context;

        public BaseRepository(ManagerContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetAsync(long id)
        {
            var obj = await _context.Set<T>()
                                .AsNoTracking()
                                .Where(x => x.Id == id)
                                .ToListAsync();

            return obj.FirstOrDefault();

        }

        public virtual async Task<List<T>> GetAsync()
        {
            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public virtual async Task<T> CreateAsync(T obj)
        {
            _context.Add(obj);

            await _context.SaveChangesAsync();

            return obj;
        }

        public async virtual Task<T> UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;

            _context.Update(obj);

            await _context.SaveChangesAsync();

            return obj;
        }

        public async virtual Task RemoveAsync(long id)
        {
            var obj = await GetAsync(id);

            if (obj is null)
                return;

            _context.Remove(obj);

            await _context.SaveChangesAsync();
        }
    }
}
