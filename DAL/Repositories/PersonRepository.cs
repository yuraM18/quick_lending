using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private quick_lendingContext db;

        public PersonRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Person item)
        {
            await db.People.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Person person = await db.People.FindAsync(id);
            if (person != null)
                db.People.Remove(person);
        }

        public async Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await db.People.Where(predicate).ToListAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            return await db.People.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await db.People.ToListAsync();
        }

        public async Task UpdateAsync(Person item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
