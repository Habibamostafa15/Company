using Company.BLL.Interfaces;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Context;

namespace Company.BLL.Reposotiry
{
    public class DepartmentReposirtory : IDepartmentRepository
    {
      
        private DBContext context;   //NULL

        public DepartmentReposirtory(DBContext dBContext) 
        
        {
            context = dBContext;
            
            }


        public IEnumerable<Department> getAll()
        {
            return context.Departments.ToList();

        }


      public  Department? GetDepartment(int id)
        {
            return context.Departments.Find(id);


        }

       public int add(Department model)
        {
            context.Departments.Add(model);
            return context.SaveChanges();

        }


      public  int Update(Department model)
        {
            context.Departments.Update(model);
            return context.SaveChanges();
        }


      public  int delete(Department model)
        {
            context.Departments.Remove(model);
            return context.SaveChanges();
        }

      

       
    }
}

