using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.DAL.Model
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your Code!")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name!")]

        public string Name { get; set; }
        [DisplayName("Date Of Creation")]

        public DateTime DateOfCreation{ get; set; }
    }
}
