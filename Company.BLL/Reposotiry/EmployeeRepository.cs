using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using Company.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposotiry
{
    public class EmployeeRepository : GenericReposirory<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(DBContext dBContext):base (dBContext)
        {
            
        }

      

    }
}
