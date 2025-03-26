using Company.BLL.Reposotiry;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Reposotiry;
using Company.BLL.Interfaces;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using NuGet.Protocol.Core.Types;
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




    }
    }

