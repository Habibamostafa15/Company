using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposotiry
{
    public class GenericReposirory<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DBContext _dBContext;

        public GenericReposirory(DBContext dBContext)
        {
            _dBContext = dBContext;
        }



        public async Task<IEnumerable<TEntity>> getAllAsync()
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                return (IEnumerable<TEntity>)await _dBContext.Employees.Include(e => e.department).ToListAsync();
            }

            return await _dBContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {     if (typeof(TEntity) == typeof(Employee))
          { 
                return  await  _dBContext.Employees.Include(E => E.department).FirstOrDefaultAsync(E => E.Id == id) as TEntity;

            }
          
            return _dBContext.Set<TEntity>().Find(id);
        }
        public async Task addAsync(TEntity model)
        {
            await _dBContext.Set<TEntity>().AddAsync(model);
        }

        public void Update(TEntity model)
        {
            _dBContext.Set<TEntity>().Update(model);
        }

        public void deletes(TEntity model)
        {
            _dBContext.Set<TEntity>().Remove(model);
        }





    }
}
