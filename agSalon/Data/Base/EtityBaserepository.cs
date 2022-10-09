using agSalon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace agSalon.Data.Base
{
    public class EtityBaseRepository<T> where T:class
    {
        private readonly AppDbContext _context;

        public EtityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

    }
}
