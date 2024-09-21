using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.DAL.Data.Contexts;
using CompanyMvc.Dox.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {

       

        public EmployeeRepository(AppDbContext dbContext): base(dbContext)
        {

            
        }

        public IEnumerable<Employee> GetEmpByName(string Name)
        {
           return  _Db.Employees.Where(E=>E.Name.ToLower().Contains(Name.ToLower())).Include(D=>D.WorkFor).ToList(); 
        }
        #region Before Re-Factor
        //private readonly AppDbContext dbContext;

        //public EmployeeRepository(AppDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return dbContext.Employees.ToList();
        //}
        //public Employee GetById(int? id)
        //{
        //   return dbContext.Employees.Find(id);
        //}
        //public int Add(Employee employee)
        //{
        //    dbContext.Employees.Add(employee);
        //    return dbContext.SaveChanges();

        //}

        //public int Update(Employee employee)
        //{
        //    dbContext.Employees.Update(employee);
        //    return dbContext.SaveChanges();
        //}


        //public int Remove(Employee employee)
        //{
        //    dbContext.Employees.Remove(employee);
        //    return dbContext.SaveChanges();
        //} 
        #endregion

    }
}
