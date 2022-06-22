﻿using DAL.Interfaces;
using DAL.Models;
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
            return await db.Employees.ToListAsync();
        }

        public async Task UpdateAsync(Employee item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public Task<IEnumerable<Employee>> GetPaginatedData(BaseFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
