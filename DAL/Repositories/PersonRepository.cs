using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private quick_lendingContext db;

        public PersonRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public void Create(Person item)
        {
            db.People.Add(item);
        }

        public void Delete(int id)
        {
            Person person = db.People.Find(id);
            if (person != null)
                db.People.Remove(person);
        }

        public IEnumerable<Person> Find(Func<Person, bool> predicate)
        {
            return db.People.Where(predicate).ToList();
        }

        public Person Get(int id)
        {
            return db.People.Find(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return db.People;
        }

        public void Update(Person item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
