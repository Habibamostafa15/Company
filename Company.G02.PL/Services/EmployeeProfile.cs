using AutoMapper;
using Company.DAL.Dtos;

namespace Company.PL.Services
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap ();



        }
    }
}
