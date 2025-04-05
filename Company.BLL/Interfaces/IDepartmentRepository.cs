using Company.DAL.Data.Context;
using Company.DAL.Entites;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Context;
namespace Company.BLL.Interfaces
{
      public  interface IDepartmentRepository : IGenericRepository<Department>
    {


    }
}
