using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<Fine> Fines { get; }
        IRepository<Person> People { get; }
        IRepository<Statement> Statements { get; }
        IRepository<StatementType> StatementTypes { get; }
        IRepository<Transaction> Transactions { get; }
        void Save();
    }
}
