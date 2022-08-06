using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int Id);
        T GetByName(string Name);
        Task<T> GetByIdAsync(int Id);

     
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();


        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);
       
        void DeleteRange(IEnumerable<T> entities);
        T Update(T entity);

        int GetCount(Expression<Func<T, bool>> criteria);
        T Find(Expression<Func<T, bool>> criteria);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria);
    }
}
