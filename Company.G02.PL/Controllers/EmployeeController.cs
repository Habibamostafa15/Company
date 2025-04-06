using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Reposotiry;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(  IUnitOfWork unitOfWork,IMapper mapper )
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]  
        public async Task<IActionResult> Index (String SearchInput)
        {
            IEnumerable<Employee> employees;
             if (String.IsNullOrEmpty(SearchInput)) {

                employees = await _unitOfWork.EmployeeRepository.getAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);

            }


            //dictionary: 3property
            //1- DataView:transfer Extra info from Controller to View
            //  ViewData["Massage"] = "Hello from ViewData";
            //2- ViewBag :transfer Extra info from Controller to View
            // ViewBag.Massage = "Hello from ViewData";
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           


            var departments =await _unitOfWork.DepartmentRepository.getAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
               if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }

                var Employee = _mapper.Map<Employee>(model);

               await  _unitOfWork.EmployeeRepository.addAsync(Employee);
                var count =  await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Massage"] = "Employee is Created successfully!!!";
                    return RedirectToAction(nameof(Index));

                }
                  
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest("InValid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with id {id} is not found" });

            var dto = new CreateEmployeeDto
            {
                Name = employee.Name,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
            return View(dto);
        }
        



        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string viewName ="Edit")
        {
          
            if (id is null) return BadRequest("InValid Id");
          
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var departments = await _unitOfWork.DepartmentRepository.getAllAsync();
            ViewData["departments"] = departments;
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            var Dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName,Dto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model, string viewName = "Edit")
        {
            if (model.ImageName is not null && model.Image is not null)
            {
                DocumentSettings.Delete(model.ImageName, "images");

            }

            if (model.Image is not null)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

            }
          
            if (ModelState.IsValid)
            {

                var employee = _mapper.Map<Employee>(model);
           _unitOfWork.EmployeeRepository.Update(employee);
                var count =  await _unitOfWork.CompleteAsync();

                if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                

            }


            return View(viewName,model);
        }


        [HttpGet]
        public async Task<IActionResult> Deletes(int? id)
        {
            //if (id is null) return BadRequest("InValid Id");
            //var department = _departmentReposirtory.GetDepartment(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return await Edit(id , "Deletes");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletes([FromRoute] int id,  CreateEmployeeDto model )
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);

                employee.Id = id;
                
                   _unitOfWork.EmployeeRepository.deletes(employee);
                    var count = await _unitOfWork.CompleteAsync();

                    if (count > 0)
                    {

                    if (model.Image is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");

                    }
                    return RedirectToAction(nameof(Index));
                    }
                

            }

            return View(model);
        }


    }
}
