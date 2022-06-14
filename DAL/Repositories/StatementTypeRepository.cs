using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class StatementTypeRepository : IRepository<StatementType>
    {
        private quick_lendingContext db;

        public StatementTypeRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public void Create(StatementType item)
        {
            db.StatementTypes.Add(item);
        }

        public void Delete(int id)
        {
            StatementType type = db.StatementTypes.Find(id);
            if (type != null)
                db.StatementTypes.Remove(type);
        }

        public IEnumerable<StatementType> Find(Func<StatementType, bool> predicate)
        {
            return db.StatementTypes.Where(predicate).ToList();
        }

        public StatementType Get(int id)
        {
            return db.StatementTypes.Find(id);
        }

        public IEnumerable<StatementType> GetAll()
        {
            return db.StatementTypes;
        }

        public void Update(StatementType item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
