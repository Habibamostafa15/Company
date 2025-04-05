using Company.DAL.Dtos;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository <T> where T :BaseEntity
    {
        IEnumerable<T> getAll();
        T? Get(int id);   
       void add(T model);
        void Update(T model);
        void delete(T model);
    }
}
