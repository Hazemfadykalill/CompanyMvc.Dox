using CompanyMvc.Dox.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Interfaces
{
    public  interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();

        //Get Employee By Id
        T GetById(int? id);

        //Add Employee
        int Add(T entity);
        //Update Employee

        int Update(T entity);
        //Delete Employee

        int Remove(T entity);

    }
}
