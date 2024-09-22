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
       Task<IEnumerable<T>>  GetAllAsync();

        //Get Employee By Id
        Task<T?> GetByIdAsync(int? id);

        //Add Employee
        Task<int> AddAsync(T entity);
        //Update Employee

        int Update(T entity);
        //Delete Employee

        int Remove(T entity);

    }
}
