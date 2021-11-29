using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ofmark.Repository.Abstract
{
    public interface IRepository<T> where T : class
    {
        bool Create(T entity);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> expression);        
        T Find(Expression<Func<T, bool>> expression);
        bool Update(T entity);
        bool Delete(T entity);                       
        bool Save();              
    }
}
