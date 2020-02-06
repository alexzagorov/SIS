using System;

namespace SulsApp
{
    public static class Program
    {
        public static void Main()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }
    }
}
