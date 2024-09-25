using AutoMapper;
using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Units;
using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.HelperLogic;
using CompanyMvc.Dox.PL.ViewModels.Employee;
using CompanyMvc.Dox.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CompanyMvc.Dox.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		// Operations On New User [ Get GetAll Details Delete Update ]
		public async Task<IActionResult> Index(string InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();
			if (InputSearch.IsNullOrEmpty())
			{
				users = await userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = userManager.GetRolesAsync(U).Result,
				}).ToListAsync();


			}
			else
			{
				users = await userManager.Users.Where(U => U.Email.ToLower().Contains(InputSearch.ToLower())).Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = userManager.GetRolesAsync(U).Result,
				}).ToListAsync();
			}

			return View(users);
		}


        public async Task<IActionResult> Details(string? id, string NameView = "Details")

        {

            if (id is null) return BadRequest();
			var userFromDb =await userManager.FindByIdAsync(id);
            if (userFromDb is null) return NotFound();

			var user = new UserViewModel()
			{
				Id= userFromDb.Id,
				FirstName = userFromDb.FirstName,
					LastName = userFromDb.LastName,
					Roles=await userManager.GetRolesAsync(userFromDb)
            };
            
            return View(NameView, user);

        }


        //Update

        public async Task<IActionResult> Update(string? id)

        {

         
            return await Details(id, "Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)

        {
            try
            {

                if (id != model.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

              
                    var userFromDb = await userManager.FindByIdAsync(id);
                    if (userFromDb is null) return NotFound();

                
                        userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;
                    var result = await userManager.UpdateAsync(userFromDb);
                   
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
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)

        {
            try
            {
                if (id != model.Id) return BadRequest()//400
         ;
                if (ModelState.IsValid)
                {


                    var userFromDb = await userManager.FindByIdAsync(id);
                    if (userFromDb is null) return NotFound();


                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;
                    var result = await userManager.DeleteAsync(userFromDb);

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
