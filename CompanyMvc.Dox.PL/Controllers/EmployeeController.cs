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
        public async Task<IActionResult> Index(string InputSearch)
        {
            var AllEmps = Enumerable.Empty<Employee>();
            if (InputSearch.IsNullOrEmpty())
            {

                AllEmps = await unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                AllEmps = await unitOfWork.EmployeeRepository.GetEmpByNameAsync(InputSearch);
            }

            var Result = mapper.Map<IEnumerable<EmployeeViewModel>>(AllEmps);
            return View(Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["departments"] = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    model.ImageName = DocumentSettings.UploadingFile(model.Image!, "Images");


                    var employee = mapper.Map<Employee>(model);
                    var count = await unitOfWork.EmployeeRepository.AddAsync(employee);

                    return RedirectToAction(nameof(Index));



                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(String.Empty, Ex.Message);

                }


            }


            return View(model);

        }

        public async Task<IActionResult> Details(int? id, string NameView = "Details")

        {

            if (id is null) return BadRequest();
            var model = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
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
            var employeeViewModel = mapper.Map<EmployeeViewModel>(model);
            return View(NameView, employeeViewModel);

        }

        //Update

        public async Task<IActionResult> Update(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();
            var department = await unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = department;
            return await Details(id, "Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update([FromRoute] int? id, EmployeeViewModel model)

        {
            try
            {

                if (id != model.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

                    //Images Update
                    if (model.ImageName is not null)
                    {

                        DocumentSettings.DeletingFile(model.ImageName, "Images");
                    }
                    if (model.Image is not null)
                    {

                        model.ImageName = DocumentSettings.UploadingFile(model.Image, "Images");
                    }

                    //2. auto mapping
                    var employee = mapper.Map<Employee>(model);
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
        public async Task<IActionResult> Delete(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();

            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel model)

        {
            if (id != model.Id) return BadRequest(); //400
            try
            {
        ;
                if (ModelState.IsValid)
                {
                    var employee = mapper.Map<Employee>(model);

                    var count = unitOfWork.EmployeeRepository.Remove(employee);
                    if (count > 0 && model.ImageName is not null)
                    {
                            DocumentSettings.DeletingFile(model.ImageName, "Images");
                       
                    }
                        return RedirectToAction(actionName: "Index");
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
