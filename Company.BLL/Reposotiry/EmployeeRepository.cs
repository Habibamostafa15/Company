using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using Company.DAL.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposotiry
{
    public class EmployeeRepository : GenericReposirory<Employee>,IEmployeeRepository
    {
        private readonly DBContext _dBContext;
      

        public EmployeeRepository(DBContext dBContext):base (dBContext)
        {
            _dBContext = dBContext;
        }

    
        public async Task<List<Employee>> GetByNameAsync(string Name)
        {
            return await _dBContext.Employees.Include(E=>E.department).Where(E => E.Name.ToLower().Contains(Name.ToLower())).ToListAsync(); 

        }

        public async Task<List<Employee>> GetLastEmployeesAsync(int count)
        {
            return await _dBContext.Employees
                .Include(e => e.department)
                .OrderByDescending(e => e.CreateAt) // or HiringDate, based on what “recent” means to you
                .Take(count)
                .ToListAsync();
        }



    }
}
