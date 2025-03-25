using Company.BLL.Reposotiry;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Reposotiry;
using Company.BLL.Interfaces;
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
          //  DepartmentReposirtory departmentReposirtory = new DepartmentReposirtory();
            //var Deparmets = _departmentReposirtory.
          // var Deparmets = departmentReposirtory.getAll();
            return View();
        }
    }
}
