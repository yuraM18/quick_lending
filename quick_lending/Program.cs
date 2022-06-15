using DAL;
using DAL.Interfaces;
using System;
using System.Linq;

namespace quick_lending
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using (quick_lendingContext db = new quick_lendingContext())
            {
                var users = db.StatementTypes.ToList();
                foreach(StatementType p in users)
                {
                    Console.WriteLine(p.Name);
                }
            }
            Console.ReadKey();
        }
    }
}
