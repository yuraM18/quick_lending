using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class FineRepository : IRepository<Fine>
    {
        private quick_lendingContext db;

        public FineRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public void Create(Fine item)
        {
            db.Fines.Add(item);
        }

        public void Delete(int id)
        {
            Fine fine = db.Fines.Find(id);
            if (fine != null)
                db.Fines.Remove(fine);
        }

        public IEnumerable<Fine> Find(Func<Fine, bool> predicate)
        {
            return db.Fines.Where(predicate).ToList();
        }

        public Fine Get(int id)
        {
            return db.Fines.Find(id);
        }

        public IEnumerable<Fine> GetAll()
        {
            return db.Fines;
        }

        public void Update(Fine item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
