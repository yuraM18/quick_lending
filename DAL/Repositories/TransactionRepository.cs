using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private quick_lendingContext db;

        public TransactionRepository(quick_lendingContext context)
        {
            this.db = context;        }

        public void Create(Transaction item)
        {
            db.Transactions.Add(item);
        }

        public void Delete(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction != null)
                db.Transactions.Remove(transaction);
        }

        public IEnumerable<Transaction> Find(Func<Transaction, bool> predicate)
        {
            return db.Transactions.Where(predicate).ToList();
        }

        public Transaction Get(int id)
        {
            return db.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return db.Transactions;
        }

        public void Update(Transaction item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
