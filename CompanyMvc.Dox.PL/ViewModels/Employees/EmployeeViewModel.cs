using CompanyMvc.Dox.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.Dox.PL.ViewModels.Employee
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name!!")]
        public string Name { get; set; }

        [Range(10, 80, ErrorMessage = "Enter Your Age Between 10 To 80")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please Enter Your Salary!!")]
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        [RegularExpression(pattern: @"[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$")]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        public Boolean IsActivated { get; set; }

        public DateTime HiringDate { get; set; }
        public int WorkForId { get; set; }
        public Department? WorkFor { get; set; }
     
        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }
    }
}
