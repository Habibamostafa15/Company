using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Reposotiry;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeReposirtory;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository EmployeeRepository
            ,IDepartmentRepository departmentRepository
            ,IMapper mapper
            )
        {
            _EmployeeReposirtory = EmployeeRepository;
            _departmentRepository = departmentRepository;
           _mapper = mapper;
        }

        [HttpGet]  
        public IActionResult Index (String SearchInput)
        {
            IEnumerable<Employee> employees;
             if (String.IsNullOrEmpty(SearchInput)) {

                employees = _EmployeeReposirtory.getAll();

            }
            else
            {
                employees = _EmployeeReposirtory.GetByName(SearchInput);
            }


                //dictionary: 3property
                //1- DataView:transfer Extra info from Controller to View
                //  ViewData["Massage"] = "Hello from ViewData";
                //2- ViewBag :transfer Extra info from Controller to View
                // ViewBag.Massage = "Hello from ViewData";
                return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.getAll();
            ViewData["departments"] = departments;
            return View();
        }
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //var Employee = new Employee
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId
                //};

                var Employee = _mapper.Map<Employee>(model);

                int count = _EmployeeReposirtory.add(Employee);
                if (count > 0)
                {
                    TempData["Massage"] = "Employee is Created successfully!!!";
                    return RedirectToAction(nameof(Index));

                }
                  
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {    
            if (id is null) return BadRequest("InValid Id");
            var employee = _EmployeeReposirtory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with id {id} is not found" });
            return View(viewName, employee);

        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.getAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("InValid Id");
            var employee = _EmployeeReposirtory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            var EmployeeDTO = new CreateEmployeeDto
            {
               
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
            return View(EmployeeDTO);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // if (id == employee.Id) return BadRequest();

                var Employee = new Employee
                {    Id=id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary
                };
                var count = _EmployeeReposirtory.Update(Employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                

            }


            return View(model);
        }


        [HttpGet]
        public IActionResult Deletes(int? id)
        {
            //if (id is null) return BadRequest("InValid Id");
            //var department = _departmentReposirtory.GetDepartment(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return Details(id, "Deletes");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletes([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                    var count = _EmployeeReposirtory.delete(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }

            return View(employee);
        }


    }
}
