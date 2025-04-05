﻿using Company.BLL.Interfaces;
using Company.BLL.Reposotiry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Context;
namespace Company.BLL
{
   public  class UnitOfWork : IUnitOfWork

    {
        private readonly DbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork( DBContext dbContext )
        {
           _dbContext = dbContext;
            DepartmentRepository = new DepartmentReposirtory(dbContext);
            EmployeeRepository = new EmployeeRepository(dbContext);
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

