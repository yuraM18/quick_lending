using DAL;
using System;
using System.Linq;

namespace quick_lending
{
    class Program
    {
        static void Main(string[] args)
        {
            using(quick_lendingContext db = new quick_lendingContext())
            {
                var users = db.People.ToList();
                foreach(Person p in users)
                {
                    Console.WriteLine(p.Email);
                }
            }
            Console.ReadKey();
        }
    }
}
