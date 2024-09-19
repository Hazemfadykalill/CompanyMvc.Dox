using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.DAL.Data.Contexts;
using CompanyMvc.Dox.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

  

        public DepartmentRepository(AppDbContext DbContext):base(DbContext)//ask clr to create object from this class before use it 
        {
        
        }
        #region Before Re factor
        //readonly AppDbContext _Db;  //Null;
        ////دلوقتي ذلك الكلاس معتمد علي كلاس اخى فمن الممكن ذلك الكلاس يجي ليا ب فاضي فلاز احقن ذلك الكلاس 

        //public DepartmentRepository(AppDbContext appDbContext)//ask clr to create object from this class before use it 
        //{ 
        //    _Db = appDbContext;

        //}
        //public IEnumerable<Department> GetAll()
        //{
        //    return _Db.Departments.ToList();

        //}

        //public Department GetById(int? id)
        //{
        //    return _Db.Departments.Find(id);
        //}
        //public int Add(Department department)
        //{
        //    _Db.Departments.Add(department);
        //    return _Db.SaveChanges();
        //}



        //public int Update(Department department)
        //{
        //    _Db.Departments.Update(department);
        //    return _Db.SaveChanges();
        //}
        //public int Remove(Department department)
        //{
        //    _Db.Departments.Remove(department);
        //    return _Db.SaveChanges();
        //} 
        #endregion

    }
}
