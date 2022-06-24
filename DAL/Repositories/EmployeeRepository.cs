using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private quick_lendingContext db;

        public EmployeeRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Employee item)
        {
            await db.Employees.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee != null)
                db.Employees.Remove(employee);
        }

        public IQueryable<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            return db.Employees.AsNoTracking().Where(predicate);
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await db.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            //IQueryable<Person> query = db.People.AsQueryable().Join(db.Employees, c => c.Id, p => p.PeopleId,
            //    (p, c) => new Person()
            //    {
            //        LastName = p.LastName,
            //        FirstName = p.FirstName,
            //        Employee = new Employee() { Id = c.Id, PeopleId = p.Id }
            //    });
            IQueryable<Employee> query = db.Employees.AsQueryable().Join(db.People, c => c.PeopleId, p => p.Id,
                (p, c) => new Employee
                {
                    Id = p.Id,
                    PeopleId = p.PeopleId,
                    People = new Person { FirstName = c.FirstName, LastName = c.LastName }
                });
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Employee item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
