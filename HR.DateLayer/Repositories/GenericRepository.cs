using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HR.DateLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        DbContext context;
        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }
        public T Get(object key)
        {
            return context.Set<T>().Find(key);
        }
        public T Add(T obj)
        {
            //AddorUpdate b aspnet core нема
            context.Set<T>().Add(obj);
            context.SaveChanges();
            return obj;
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().AsExpandable().Where(predicate); //AsExpandable() => using LinqKit; приміняє предикат на клієнтській стороні
        }


        public T Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
            return obj;

        }

        public T Update(T obj, object key)
        {
            if (obj == null) return null;
            //T obj не належить контексту і при виклику напряму метода Update для obj буде генеруватись exception
            T exist = context.Set<T>().Find(key);
            if(exist!=null)
            {
                context.Entry(exist).CurrentValues.SetValues(obj);
                context.SaveChanges();
            }
            return exist;
        }
    }
}
