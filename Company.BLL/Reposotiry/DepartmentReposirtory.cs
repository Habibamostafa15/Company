using Company.BLL.Interfaces;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Reposotiry
{
    public class DepartmentReposirtory : GenericReposirory<Department>,IDepartmentRepository
    {


        public DepartmentReposirtory(DBContext dBContext) :base (dBContext)
        {
            
        }

    
    }
}


