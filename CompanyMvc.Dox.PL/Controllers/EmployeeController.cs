using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _repository = employeeRepository;
            this.departmentRepository = departmentRepository;
        }
        //[HttpGet]
        public IActionResult Index(string InputSearch)
        {
            var AllEmps = Enumerable.Empty<Employee>();
            if (InputSearch.IsNullOrEmpty())
            {

                AllEmps = _repository.GetAll();
            }
            else
            {
                AllEmps = _repository.GetEmpByName(InputSearch);
            }
            return View(AllEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["departments"] = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //casting from empViewModel (ViewModel) To EmpModel (Employee)
                    //Mapping
                    //1.Manual Mapping

                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        Salary = model.Salary,
                        HiringDate = model.HiringDate,
                        IsActivated = model.IsActivated,
                        WorkFor = model.WorkFor,
                        WorkForId = model.WorkForId,
                        phoneNumber = model.phoneNumber



                    };
                    var count = _repository.Add(employee);
                  
                    return RedirectToAction(nameof(Index));
                 

                    //2.Automatic Mapping
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(String.Empty, Ex.Message);

                }


            }


            return View(model);

        }

        public IActionResult Details(int? id, string NameView = "Details")

        {

            if (id is null) return BadRequest();
            var model = _repository.GetById(id); 
            if (model is null) return NotFound();
            //casting from empViewModel (ViewModel) To EmpModel (Employee)
            //Mapping
            //1.Manual Mapping

            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Age = model.Age,
                Email = model.Email,
                Salary = model.Salary,
                HiringDate = model.HiringDate,
                IsActivated = model.IsActivated,
                WorkFor = model.WorkFor,
                WorkForId = model.WorkForId,
                phoneNumber = model.phoneNumber



            };
            return View(NameView, employeeViewModel);

        }

        //Update

        public IActionResult Update(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();
            var department = departmentRepository.GetAll();
            ViewData["departments"] = department;
            return Details(id, "Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update([FromRoute] int? id, EmployeeViewModel model)

        {
            try
            {

                if (id !=model.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {
                    //casting from empViewModel (ViewModel) To EmpModel (Employee)
                    //Mapping
                    //1.Manual Mapping

                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        Salary = model.Salary,
                        HiringDate = model.HiringDate,
                        IsActivated = model.IsActivated,
                        WorkFor = model.WorkFor,
                        WorkForId = model.WorkForId,
                        phoneNumber = model.phoneNumber



                    };
                    var count = _repository.Update(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(actionName: "Index");
                    }
                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }


            return View(model);

        }


        //Delete
        [HttpGet]
        public IActionResult Delete(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();

            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel model)

        {
            try
            {
                if (id != model.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {
                    //casting from empViewModel (ViewModel) To EmpModel (Employee)
                    //Mapping
                    //1.Manual Mapping

                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        Salary = model.Salary,
                        HiringDate = model.HiringDate,
                        IsActivated = model.IsActivated,
                        WorkFor = model.WorkFor,
                        WorkForId = model.WorkForId,
                        phoneNumber = model.phoneNumber



                    };
                    var count = _repository.Remove(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(actionName: "Index");
                    }
                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }


            return View(model);

        }

    }
}
