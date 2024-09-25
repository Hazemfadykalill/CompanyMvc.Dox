using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.Dox.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Please Enter Your Password!!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please Enter Your ConfirmedPassword!!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "ConfirmedPassword Must Equal Password value")]
		public string ConfirmedPassword { get; set; }
	}
}
