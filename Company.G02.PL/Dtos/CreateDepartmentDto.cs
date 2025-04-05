using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Dtos
{
  public  class CreateDepartmentDto: BaseEntity
    {
  
        [Required (ErrorMessage ="Code is requierd")]
        public string code { set; get; }
        [Required(ErrorMessage = "Name is requierd")]

        public string name { set; get; }
        [Required(ErrorMessage = "Create at is requierd")]

        public DateTime createAt { set; get; }
    }
}
