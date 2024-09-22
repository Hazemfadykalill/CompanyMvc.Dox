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
        public async Task<IActionResult> Index()
        {

            var AllDepartments = await _repository.GetAllAsync();

            return View(AllDepartments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {

                var count = await _repository.AddAsync(department);
                if (count > 0)
                {
                    return RedirectToAction(actionName: "Index");
                }
            }


            return View(department);

        }

        public async Task<IActionResult> Details(int? id,string NameView= "Details")

        {

            if (id is null) return BadRequest();
            var department =await _repository.GetByIdAsync(id);
            if (department is null) return NotFound();

            return View(NameView,department);

        }

        //Update

        public async Task<IActionResult> Update(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();

            return await Details(id,"Update");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

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


        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)

        {

            //if (id is null) return BadRequest();
            //var department = _repository.GetById(id);
            //if (department is null) return NotFound();

            return await Details(id,"Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Department department)

        {
            try
            {
                if (id != department.Id) return BadRequest()//400
        ;
                if (ModelState.IsValid)
                {

                    var count = _repository.Remove(department);
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
