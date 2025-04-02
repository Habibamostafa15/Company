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
        int add(T model);
        int Update(T model);
        int delete(T model);
    }
}
