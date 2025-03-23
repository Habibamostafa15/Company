using Company.DAL.Entites;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
      public  interface IDepartmentRepository
    {
         IEnumerable<Department> getAll();  
        Department? GetDepartment(int id);   // make it nullable 
        int add(Department model);
        int Update(Department model);
        int delete(Department model);


    }
}
