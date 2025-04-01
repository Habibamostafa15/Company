using Company.DAL.Dtos;
using Company.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions <DBContext> options ) : base (options)
            {    


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=companyG02;Trusted_Connection=True;TrustServerCertificate = True");


        //}


     

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee>   Employees { get; set; }

    }
}
