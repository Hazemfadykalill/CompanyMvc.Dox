﻿using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.ViewModels.Roles;
using CompanyMvc.Dox.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManger;

        public RoleController(RoleManager<IdentityRole> roleManger)
        {
            this.roleManger = roleManger;
        }

        // Operations On New User [ Get GetAll Details Delete Update  create]
        public async Task<IActionResult> Index(string InputSearch)
        {
            var users = Enumerable.Empty<RoleViewModel>();
            if (InputSearch.IsNullOrEmpty())
            {
                users = await roleManger.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name!
                }).ToListAsync();


            }
            else
            {
                users = await roleManger.Roles.Where(R => R.Name.ToLower().Contains(InputSearch.ToLower())).Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name!
                }).ToListAsync();

            }

            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role=new IdentityRole()
                {

                  
                    Name=model.RoleName,

                };
                var Result=await roleManger.CreateAsync(role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");   
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string NameView = "Details")

        {

            if (id is null) return BadRequest();
            var roleFromDb = await roleManger.FindByIdAsync(id);
            if (roleFromDb is null) return NotFound();

            var role = new RoleViewModel()
            {
                Id = roleFromDb.Id,
                RoleName = roleFromDb.Name!
            };

            return View(NameView, role);

        }


        //Update

        public async Task<IActionResult> Update(string? id)

        {


            return await Details(id, "Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update([FromRoute] string? id, RoleViewModel model)

        {
            try
            {

                if (id != model.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {


                    var roleFromDb = await roleManger.FindByIdAsync(id);
                    if (roleFromDb is null) return NotFound();


                    roleFromDb.Name = model.RoleName;
                
                    var result = await roleManger.UpdateAsync(roleFromDb);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "Index");

                    }

                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }


            return View(model);

        }


        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)

        {



            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)

        {
            try
            {
                if (id != model.Id) return BadRequest()//400
         ;
                if (ModelState.IsValid)
                {


                    var roleFromDb = await roleManger.FindByIdAsync(id);
                    if (roleFromDb is null) return NotFound();


                     
                    var result = await roleManger.DeleteAsync(roleFromDb);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "Index");

                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "Index");

                    }

                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }


            return View(model);

        }
    }
}
