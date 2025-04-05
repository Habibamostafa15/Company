using Company.DAL.Dtos;
using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity

    {

        Task<IEnumerable<TEntity>> getAllAsync();
        Task<TEntity?> GetAsync(int id);
        Task addAsync(TEntity model);
        void Update(TEntity model);
        void deletes(TEntity model);




        // IEnumerable<> getAllAsync();
        // T? Get(int id);   
        //void add(T model);
        // void Update(T model);
        // void delete(T model);
    }
}
