using DataLib.Models;
using System.Data.Entity;

namespace DataLib
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DbConnection")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
