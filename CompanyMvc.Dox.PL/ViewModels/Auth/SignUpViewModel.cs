


using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.Dox.PL.ViewModels.Auth
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "Please Enter Your UserName!!")]

		public string UserName { get; set; }
		[Required(ErrorMessage = "Please Enter Your FirstName!!")]

		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please Enter Your LastName!!")]

		public string LastName { get; set; }
		[Required(ErrorMessage = "Please Enter Your Email!!")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please Enter Your Password!!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please Enter Your ConfirmedPassword!!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="ConfirmedPassword Must Equal Password value")]
		public string ConfirmedPassword { get; set; }
		public bool IsAgree { get; set; }


	}
}
