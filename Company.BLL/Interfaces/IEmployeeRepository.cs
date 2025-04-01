using Company.DAL.Dtos;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
  public interface IEmployeeRepository
    {

        IEnumerable<Employee> getAll();
        Employee? Get(int id);   // make it nullable 
        int add(Employee model);
        int Update(Employee model);

        int delete(Employee model);


    }
}
