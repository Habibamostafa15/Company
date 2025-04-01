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
       private readonly IDepartmentRepository _departmentReposirtory;

        //ask CLR create object from DI
        public DepartmentController(IDepartmentRepository DepartmentReposirtory)
        {
            _departmentReposirtory =  DepartmentReposirtory;   //هتشاور علي الاوبجكت اللي عمله الclr
        }

        [HttpGet]  // baseURl/Department/Index
        public IActionResult Index()
        { 
           var Deparmets = _departmentReposirtory.getAll();
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
                int count = _departmentReposirtory.add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        public IActionResult Details(int? id)
        { if (id is null) return BadRequest("InValid Id");
            var department = _departmentReposirtory.GetDepartment(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            return View(department);

        }
    }
    }

