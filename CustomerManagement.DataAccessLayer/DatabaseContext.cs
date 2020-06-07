using CustomerManagement.Entities;
using CustomerManagement.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomerManagement.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<CMUser> CMUser { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Incident> Incident { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
