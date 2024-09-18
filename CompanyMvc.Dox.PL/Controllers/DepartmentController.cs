using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var AllDepartments = _repository.GetAll();

            return View(AllDepartments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {

                var count = _repository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(actionName: "Index");
                }
            }


            return View(department);

        }

        public IActionResult Details(int? id)

        {

            if (id is null) return BadRequest();
            var department = _repository.GetById(id);
            if (department is null) return NotFound();

            return View(department);

        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int?id,Department department)

        {
            try
            {
                if (id != department.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

                    var count = _repository.Update(department);
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


            return View(department);    

        }
    }
}
