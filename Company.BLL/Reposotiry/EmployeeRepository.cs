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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext _dBContext;

        public EmployeeRepository(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }


        public IEnumerable<Employee> getAll()
        {
            return _dBContext.Employees.ToList();
        }

        public Employee? Get(int id)
        {
            return _dBContext.Employees.Find(id);
        }


        public int add(Employee model)
        {
            _dBContext.Employees.Add(model);
            return _dBContext.SaveChanges();
        }

        public int Update(Employee model)
        {
            _dBContext.Employees.Update(model);
            return  _dBContext.SaveChanges();
        }
        public int delete(Employee model)
        {
            _dBContext.Employees.Remove(model);
            return _dBContext.SaveChanges();
        }

      

      
    }
}
