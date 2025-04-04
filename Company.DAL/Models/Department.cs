using Company.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entites
{
     public   class Department : BaseEntity
    {
      public string code { set; get; }
        public string name { set; get; }
        public DateTime createAt { set; get; }
    }
}
