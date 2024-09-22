using AutoMapper;
using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.HelperLogic;
using CompanyMvc.Dox.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _repository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            //_repository = employeeRepository;
            //this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        //[HttpGet]
        public IActionResult Index(string InputSearch)
        {
            var AllEmps = Enumerable.Empty<Employee>();
            if (InputSearch.IsNullOrEmpty())
            {

                AllEmps = unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                AllEmps = unitOfWork.EmployeeRepository.GetEmpByName(InputSearch);
            }

            var Result=mapper.Map<IEnumerable<EmployeeViewModel>>(AllEmps);
            return View(Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["departments"] = unitOfWork.DepartmentRepository.GetAll();
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
                    model.ImageName = DocumentSettings.UploadingFile(model.Image,"Images");

                    //casting from empViewModel (ViewModel) To EmpModel (Employee)
                    //Mapping
                    //1.Manual Mapping

                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    HiringDate = model.HiringDate,
                    //    IsActivated = model.IsActivated,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    phoneNumber = model.phoneNumber



                    //};
                    //2.Automatic Mapping
                    var employee=mapper.Map<Employee>(model);
                    var count = unitOfWork.EmployeeRepository.Add(employee);
                  
                    return RedirectToAction(nameof(Index));
                 

                   
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
            var model = unitOfWork.EmployeeRepository.GetById(id); 
            if (model is null) return NotFound();
            //casting from empViewModel (ViewModel) To EmpModel (Employee)
            //Mapping
            //1.Manual Mapping

            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Address = model.Address,
            //    Age = model.Age,
            //    Email = model.Email,
            //    Salary = model.Salary,
            //    HiringDate = model.HiringDate,
            //    IsActivated = model.IsActivated,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    phoneNumber = model.phoneNumber



            //};

            //2.auto mapping
           var employeeViewModel=mapper.Map<EmployeeViewModel>(model);
            return View(NameView, employeeViewModel);

        }

        //Update

        public IActionResult Update(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();
            var department = unitOfWork.DepartmentRepository.GetAll();
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

                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    HiringDate = model.HiringDate,
                    //    IsActivated = model.IsActivated,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    phoneNumber = model.phoneNumber



                    //};

                    //2. auto mapping
                    var employee=mapper.Map<Employee>(model);
                    var count = unitOfWork.EmployeeRepository.Update(employee);
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

                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    HiringDate = model.HiringDate,
                    //    IsActivated = model.IsActivated,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    phoneNumber = model.phoneNumber



                    //};

                    //2.auto mapping
                    var employee = mapper.Map<Employee>(model);

                    var count = unitOfWork.EmployeeRepository.Remove(employee);
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
