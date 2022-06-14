using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StatementRepository : IRepository<Statement>
    {
        private quick_lendingContext db;

        public StatementRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Statement item)
        {
            await db.Statements.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Statement statement = await db.Statements.FindAsync(id);
            if (statement != null)
                db.Statements.Remove(statement);
        }

        public async Task<IEnumerable<Statement>> FindAsync(Expression<Func<Statement, bool>> predicate)
        {
            return await db.Statements.Where(predicate).ToListAsync();
        }

        public async Task<Statement> GetAsync(int id)
        {
            return await db.Statements.FindAsync(id);
        }

        public async Task<IEnumerable<Statement>> GetAllAsync()
        {
            return await db.Statements.ToListAsync();
        }

        public async Task UpdateAsync(Statement item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
