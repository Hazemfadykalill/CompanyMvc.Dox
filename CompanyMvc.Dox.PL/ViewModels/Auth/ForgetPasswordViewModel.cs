using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.Dox.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="This Email Is Required!!")]
        [EmailAddress(ErrorMessage ="Invalid Email!!")]
        
        public string Email { get; set; }
    }
}
