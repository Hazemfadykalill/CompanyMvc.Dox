using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var AllEmps = _repository.GetAll();

            return View(AllEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

                var count = _repository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(actionName: "Index");
                }
            }


            return View(employee);

        }

        public IActionResult Details(int? id, string NameView = "Details")

        {

            if (id is null) return BadRequest();
            var emps = _repository.GetById(id);
            if (emps is null) return NotFound();

            return View(NameView, emps);

        }

        //Update

        public IActionResult Update(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();

            return Details(id, "Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update([FromRoute] int? id, Employee emp)

        {
            try
            {
                if (id != emp.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

                    var count = _repository.Update(emp);
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


            return View(emp );

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
        public IActionResult Delete([FromRoute] int? id, Employee emp)

        {
            try
            {
                if (id != emp.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

                    var count = _repository.Remove(emp);
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


            return View(emp);

        }

    }
}
