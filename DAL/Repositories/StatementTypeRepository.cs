using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StatementTypeRepository : IRepository<StatementType>
    {
        private quick_lendingContext db;

        public StatementTypeRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(StatementType item)
        {
            await db.StatementTypes.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            StatementType type = await db.StatementTypes.FindAsync(id);
            if (type != null)
                db.StatementTypes.Remove(type);
        }

        public async Task<IEnumerable<StatementType>> FindAsync(Expression<Func<StatementType, bool>> predicate)
        {
            return await db.StatementTypes.Where(predicate).ToListAsync();
        }

        public async Task<StatementType> GetAsync(int id)
        {
            return await db.StatementTypes.FindAsync(id);
        }

        public async Task<IEnumerable<StatementType>> GetAllAsync()
        {
            return await db.StatementTypes.ToListAsync();
        }

        public async Task UpdateAsync(StatementType item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
