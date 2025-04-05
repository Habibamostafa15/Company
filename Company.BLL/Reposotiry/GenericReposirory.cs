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
    public class GenericReposirory<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DBContext _dBContext;

        public GenericReposirory(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IEnumerable<T> getAll()
        {
            return _dBContext.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return _dBContext.Set<T>().Find(id);
        }
        public void add(T model)
        {
            _dBContext.Set<T>().Add(model);
        }     

        public void Update(T model)
        {
            _dBContext.Set<T>().Update(model);
        }

        public void delete(T model)
        {
            _dBContext.Set<T>().Remove(model);
           
        }

       

       
    }
}
