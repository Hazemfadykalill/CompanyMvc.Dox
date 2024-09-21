using AutoMapper;
using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.ViewModels.Employee;

namespace CompanyMvc.Dox.PL.Mapping.Employees
{
    public class EmployeeProfile:Profile
    {

        public EmployeeProfile()
        {
            //CreateMap<Employee, EmployeeViewModel>();
            //CreateMap<EmployeeViewModel,Employee>();
            // or
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}
