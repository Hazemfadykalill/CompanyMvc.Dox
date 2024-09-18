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
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {

        readonly AppDbContext _Db;  //Null;
        //دلوقتي ذلك الكلاس معتمد علي كلاس اخى فمن الممكن ذلك الكلاس يجي ليا ب فاضي فلاز احقن ذلك الكلاس 

        public GenericRepository(AppDbContext appDbContext)//ask clr to create object from this class before use it 
        {
            _Db = appDbContext;

        }
        public IEnumerable<T> GetAll()
        {
            return _Db.Set<T>().ToList();

        }

        public T GetById(int? id)
        {
            return _Db.Set<T>().Find(id);
        }
        public int Add(T entity)
        {
            _Db.Set<T>().Add(entity);
            return _Db.SaveChanges();
        }



        public int Update(T entity)
        {
            _Db.Set<T>().Update(entity);
            return _Db.SaveChanges();
        }
        public int Remove(T entity)
        {
            _Db.Set<T>().Remove(entity);
            return _Db.SaveChanges();
        }

    }
}
