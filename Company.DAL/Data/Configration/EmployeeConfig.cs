using Company.DAL.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Configration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(12, 2)");


            builder.HasOne(E => E.department)
                    .WithMany(D => D.Employees)
                    .HasForeignKey(E => E.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
