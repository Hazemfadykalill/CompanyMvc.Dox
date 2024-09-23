using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.Dox.PL.ViewModels.Auth
{
	public class LoginViewModel
	{

		

		
		[Required(ErrorMessage = "Please Enter Your Email!!")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please Enter Your Password!!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

	}
}
