using Company.DAL.Dtos;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
      Task  < List<Employee>> GetByNameAsync(String Name);
        Task<List<Employee>> GetLastEmployeesAsync(int count);

    }
}
