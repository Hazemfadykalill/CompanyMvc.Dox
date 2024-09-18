using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.DAL.Model
{
    public  class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your Name!!")]
        public string Name { get; set; }

        [Range(10,80,ErrorMessage ="Enter Your Age Between 10 To 80")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please Enter Your Salary!!")]

        public double  Salary { get; set; }
        [RegularExpression(pattern: @"[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$")]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        public Boolean IsActivated { get; set; }
        public Boolean IsDeleted { get; set; }

        public DateTime  HiringDate { get; set; }
        public DateTime  DateOfCreation { get; set; }=DateTime.Now;
    }
}
