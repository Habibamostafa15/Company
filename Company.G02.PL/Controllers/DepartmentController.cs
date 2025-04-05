using Company.BLL.Reposotiry;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Reposotiry;
using Company.BLL.Interfaces;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using NuGet.Protocol.Core.Types;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
namespace Company.PL.Controllers
{    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentReposirtory;

        //ask CLR create object from DI
        public DepartmentController(IUnitOfWork unitOfWork)//IDepartmentRepository DepartmentReposirtory
        {
            _unitOfWork = unitOfWork;
            //   _departmentReposirtory =  DepartmentReposirtory;   //هتشاور علي الاوبجكت اللي عمله الclr
        }

        [HttpGet]  // baseURl/Department/Index
        public IActionResult Index()
        { 
           var Deparmets = _unitOfWork.DepartmentRepository.getAll();
            return View(Deparmets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
            public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    code = model.code,
                    name = model.name,
                    createAt = model.createAt
                };
                int count = _unitOfWork.DepartmentRepository.add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id,string viewName="Details")
        { if (id is null) return BadRequest("InValid Id");
            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return View(viewName,department);

        }



        [HttpGet]
        public IActionResult Edit (int? id)
        {
            //if (id is null) return BadRequest("InValid Id");
            //var department = _departmentReposirtory.GetDepartment(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return Details(id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( [FromRoute] int id ,Department department)
        {     if (ModelState.IsValid)
            {     if (id == department.Id)
                {
                    var count = _unitOfWork.DepartmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
              
            }

            return View(department);
        }


        [HttpGet]
        public IActionResult Deletes (int? id)
        {
            //if (id is null) return BadRequest("InValid Id");
            //var department = _departmentReposirtory.GetDepartment(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return Details(id,"Deletes");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletes([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _unitOfWork.DepartmentRepository.delete(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }

            return View(department);
        }



    }
}

