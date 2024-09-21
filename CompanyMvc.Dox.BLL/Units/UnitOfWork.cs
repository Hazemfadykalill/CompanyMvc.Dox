using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Units
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDepartmentRepository _DepartmentRepository;
        private IEmployeeRepository _EmployeeRepository;
        public UnitOfWork(AppDbContext context)
        {
                _EmployeeRepository=new EmployeeRepository(context);
                _DepartmentRepository=new DepartmentRepository(context);
            this.context = context;
        }
        public IEmployeeRepository EmployeeRepository => _EmployeeRepository;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository;
    }
}
