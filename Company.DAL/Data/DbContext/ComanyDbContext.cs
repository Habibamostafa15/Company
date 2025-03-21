using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.DAL.Data.DbContext
{
   internal class ComanyDbContext : DbContext
    {  public ComanyDbContext () :base
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=companyG02;Trusted_Connection=True;TrustServerCertificate = True");
        }

        public DbSet<Department> departments { get; set; }
    }
}
