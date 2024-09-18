using CompanyMvc.Dox.DAL.Model;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
      


        //IEnumerable<Employee> GetAll();

        ////Get Employee By Id
        //Employee GetById(int? id);

        ////Add Employee
        //int Add(Employee department);
        ////Update Employee

        //int Update(Employee department);
        ////Delete Employee

        //int Remove(Employee employee);
    }
}
