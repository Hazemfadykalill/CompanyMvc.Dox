using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.DAL.Model
{
    public  class Employee:BaseEntity
    {
       
        public string Name { get; set; }

        public int? Age { get; set; }
        public double  Salary { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public Boolean IsActivated { get; set; }
        public Boolean IsDeleted { get; set; }

        public DateTime  HiringDate { get; set; }
        public DateTime  DateOfCreation { get; set; }=DateTime.Now;
        public int WorkForId { get; set; }
        public Department? WorkFor { get; set; }
    }
}
