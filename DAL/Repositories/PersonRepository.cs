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
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Person person = await db.People.FirstOrDefaultAsync(p => p.Id == id);
            if (person != null)
            {
                db.People.Remove(person);
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Person> Find(Expression<Func<Person, bool>> predicate)
        {
            return db.People.AsNoTracking().Where(predicate);
        }

        public async Task<Person> GetAsync(int id)
        {
            return await db.People.AsNoTracking().FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await db.People.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Person item)
        {
            db.People.Update(item);//.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
