using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.DAL.Data.Contexts;
using CompanyMvc.Dox.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {

      private protected  readonly AppDbContext _Db;  //Null;
        //دلوقتي ذلك الكلاس معتمد علي كلاس اخى فمن الممكن ذلك الكلاس يجي ليا ب فاضي فلاز احقن ذلك الكلاس 

        public GenericRepository(AppDbContext appDbContext)//ask clr to create object from this class before use it 
        {
            _Db = appDbContext;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Employee))
            {

            return  (IEnumerable<T>) await _Db.Employees.Include(E=>E.WorkFor).ToListAsync();
            }
            return await _Db.Set<T>().ToListAsync();

        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            return await _Db.Set<T>().FindAsync(id);
        }
        public async Task<int> AddAsync(T entity)
        {
           await _Db.Set<T>().AddAsync(entity);
            return _Db.SaveChanges();
        }



        public int Update(T entity)
        {
            _Db.Set<T>().Update(entity);
            return  _Db.SaveChanges();
        }
        public int Remove(T entity)
        {
            _Db.Set<T>().Remove(entity);
            return _Db.SaveChanges();
        }

    }
}
