using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Dox.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManger;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManger,SignInManager<ApplicationUser> signInManager)
		{
			this.userManger = userManger;
			this.signInManager = signInManager;
		}
		//SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}


		//SignUp
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var User = await userManger.FindByNameAsync(model.UserName);
					if (User is null)
					{
						User = await userManger.FindByEmailAsync(model.Email);
						if (User is null)
						{
							User = new ApplicationUser()
							{
								UserName = model.UserName,
								Email = model.Email,
								FirstName = model.FirstName,
								LastName = model.LastName,
								IsAgree = model.IsAgree

							};
							var Result = await userManger.CreateAsync(User, model.Password);
							//SignUp
							if (Result.Succeeded)
							{
								return RedirectToAction(nameof(Login));
							}
							foreach (var error in Result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);


							}

						}
						ModelState.AddModelError(string.Empty, "This Email Is Already Exists!!");
						return View(model);
					}
					ModelState.AddModelError(string.Empty, "This Email Is Already Exists!!");

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);

				}
			}
			return View(model);
		}
		//Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		//Login
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					

					var User =await userManger.FindByEmailAsync(model.Email);
					if (User is not  null)
					{
					
						//Password
						var Result =await  userManger.CheckPasswordAsync(User, model.Password);
						if (Result)
						{
							//login
							var result=await signInManager.PasswordSignInAsync(User,model.Password,model.RememberMe,false);
							if (result.Succeeded)
							{

							return RedirectToAction("Index", "Home");
							}


						}


					}

					ModelState.AddModelError(string.Empty, "Invalid Login !!");


				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}


        //Log Out
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();	
			return RedirectToAction("Login");

        }

        //forget password
        public IActionResult ForgetPassword()
        {
			return View();

        }
		[HttpPost]
		public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				//this email found or no 
				var User= await userManger.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					//this email is found 
					//Send Email 
					//1. Generate Token That You Send With URL
					var Token=await userManger.GeneratePasswordResetTokenAsync(User);

					//2.Create ResetPassord URL
					var URL=Url.Action("ResetPassword","Account",new { Email = model.Email,Token=Token },Request.Scheme);

					//3.Create Email That Sending 
					var emailSending = new EmailSending()
					{
						To = model.Email,
						Body = "",
						Subject=URL

					};

				}
				ModelState.AddModelError(string.Empty, "Invalid Please Try Again!!");

			}
			return View(model);

		}


	}
}
