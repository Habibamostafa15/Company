using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }


        [HttpGet]
        public async Task<IActionResult> Index(string? searchInput)
        {
            IEnumerable<RoleToReturnDto> roles;

            if (string.IsNullOrEmpty(searchInput))
            {
                roles = _roleManager.Roles.Select(u => new RoleToReturnDto()
                {
                    Id = u.Id,
                    Name = u.Name
                });
            }
            else
            {
                roles = _roleManager.Roles.Select(u => new RoleToReturnDto()
                {
                    Id = u.Id,
                    Name = u.Name
                }).Where(r => r.Name.ToLower().Contains(searchInput.ToLower()));
            }

            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "Index");
                    }
                }

            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest(error: "Invalid ID"); // 400

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { statusCode = 404, message = $"Role with Id :{id} is not found" });

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewName, dto);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != model.Id) return BadRequest(error: "Invalid Operation !!");

        //        var role = await _roleManager.FindByIdAsync(id);
        //        if (role is null) return BadRequest(error: "Invalid Operation !!");

        //        var roleResult = await _roleManager.FindByNameAsync(model.Name);
        //        if (roleResult is null)
        //        {
        //            role.Name = model.Name;
        //            var result = await _roleManager.UpdateAsync(role);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }

        //        ModelState.AddModelError(key: "", errorMessage: "Invalid Operation !!");
        //    }

        //    return View(model);
        //}



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { statusCode = 404, message = $"Role with Id :{id} is not found" });

            var model = new RoleToReturnDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest(error: "Invalid Operation !!");

                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest(error: "Invalid Operation !!");

                var roleResult = await _roleManager.FindByNameAsync(model.Name);
                if (roleResult is null || roleResult.Id == role.Id) // Allow update if the name is the same as the current role
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role with this name already exists!");
                }
            }

            return View("Edit", model);
        }





        [HttpGet]
        public async Task<IActionResult> Deletes(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid Id!");

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound($"Role with Id {id} not found");

            var dto = new RoleToReturnDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(dto);
        }




        [HttpPost]
        public async Task<IActionResult> Deletes(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role is null)
                    return NotFound("Role Not Found");

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Delete Failed");
            }

            return View(model);
        }







    }
}


