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

        public DepartmentReposirtory() 
        
        {
            context = new DBContext();
            
            }

       
        IEnumerable<Department> IDepartmentRepository.getAll()
        {
            return context.Departments.ToList();

        }


        Department? IDepartmentRepository.GetDepartment(int id)
        {
            return context.Departments.Find(id);


        }

        int IDepartmentRepository.add(Department model)
        {
            context.Departments.Add(model);
            return context.SaveChanges();

        }


        int IDepartmentRepository.Update(Department model)
        {
            context.Departments.Update(model);
            return context.SaveChanges();
        }


        int IDepartmentRepository.delete(Department model)
        {
            context.Departments.Remove(model);
            return context.SaveChanges();
        }

      

       
    }
}

