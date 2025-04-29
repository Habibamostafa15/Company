using Company.BLL.Reposotiry;
using Microsoft.AspNetCore.Mvc;
//using Company.BLL.Reposotiry;
using Company.BLL.Interfaces;
using Company.DAL.Dtos;
using Company.DAL.Entites;
using NuGet.Protocol.Core.Types;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
namespace Company.PL.Controllers
{    // MVC Controller

    [Authorize]
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
        public async Task<IActionResult> Index()
        { 
           var Deparmets =await _unitOfWork.DepartmentRepository.getAllAsync();
            return View(Deparmets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
            public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    code = model.code,
                    name = model.name,
                    createAt = model.createAt
                };
           await   _unitOfWork.DepartmentRepository.addAsync(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest("InValid Id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });

            var dto = new CreateDepartmentDto
            {
                code = department.code,
                name = department.name,
                createAt = department.createAt
            };
            return View(dto);
        }



        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if (id is null) return BadRequest("InValid Id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            var dto = new CreateDepartmentDto()
            {
                name = department.name,
                code= department.code,
                createAt = department.createAt
            };


            return View(dto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [FromRoute] int id ,CreateDepartmentDto model)
        {     if (ModelState.IsValid)

            {
                var department = new Department()
                {
                    Id = id,
                    name = model.name,
                    code = model.code,
                    createAt = model.createAt
                };



                _unitOfWork.DepartmentRepository.Update(department);

                    var count = await _unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                
              
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Deletes (int? id)
        {
            if (id is null) return BadRequest("InValid Id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"department with id {id} is not found" });
            var dto = new CreateDepartmentDto()
            {
                name = department.name,
                code = department.code,
                createAt = department.createAt
            };
            return View(dto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletes([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Id = id,
                    code = model.code,
                    name = model.name,
                    createAt = model.createAt
                };
                _unitOfWork.DepartmentRepository.deletes(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }



    }
}

