using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.HelperLogic;
using CompanyMvc.Dox.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
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
            if (ModelState.IsValid)//server side validation
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


                    var User = await userManger.FindByEmailAsync(model.Email);
                    if (User is not null)
                    {

                        //Password
                        var Result = await userManger.CheckPasswordAsync(User, model.Password);
                        if (Result)
                        {
                            //login
                            var result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);//this PasswordSignInAsync that create token with each login ya am hazem
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
            await signInManager.SignOutAsync(); //this line delete My token that it emit enter to web page .
            return RedirectToAction("Login");

        }

        //forget password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SendEmails(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //this email found or no 
                var User = await userManger.FindByEmailAsync(model.Email!);
                if (User is not null)
                {
                    //this email is found 

                    //1. Generate Token That You Send With URL
                    var Token = await userManger.GeneratePasswordResetTokenAsync(User);

                    //2.Create ResetPassord URL
                    var URL = Url.Action("ResetPassword", "Account", new { model.Email, Token }, Request.Scheme);

                    //3.Create Email That Sending 
                    var emailSending = new EmailSending()
                    {
                        To = model.Email!,
                        Body = URL!,
                        Subject = "Reset Password"

                    };
                    //Send Email 
                    EmailSettings.SendEmail(emailSending);
                    return RedirectToAction(nameof(CheckYourInBox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Please Try Again!!");

            }
            return View("ForgetPassword", model);

        }
        //view that show during sending mail  
        public IActionResult CheckYourInBox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var Email = TempData["Email"] as string;
                var Token = TempData["Token"] as string;
                var user = await userManger.FindByEmailAsync(Email!);
                if (user is not null)
                {

                    var result = await userManger.ResetPasswordAsync(user, Token!, model.Password!);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);

                        }
                    }
                } 
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
