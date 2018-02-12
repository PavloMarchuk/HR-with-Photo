using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HR.DateLayer.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Get(object key);
        T Add(T obj);
        T Update(T obj, object key); //ключі можуть бути різного типу і можуть бути "составными"
        T Delete(T obj);
    }
}
