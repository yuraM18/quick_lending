using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class StatementRepository : IRepository<Statement>
    {
        private quick_lendingContext db;

        public StatementRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public void Create(Statement item)
        {
            db.Statements.Add(item);
        }

        public void Delete(int id)
        {
            Statement statement = db.Statements.Find(id);
            if (statement != null)
                db.Statements.Remove(statement);
        }

        public IEnumerable<Statement> Find(Func<Statement, bool> predicate)
        {
            return db.Statements.Where(predicate).ToList();
        }

        public Statement Get(int id)
        {
            return db.Statements.Find(id);
        }

        public IEnumerable<Statement> GetAll()
        {
            return db.Statements;
        }

        public void Update(Statement item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
