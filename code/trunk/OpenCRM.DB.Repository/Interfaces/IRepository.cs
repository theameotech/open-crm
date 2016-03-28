using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OpenCRM.DB.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);

        IList<T> FetchAll();

        void Update(T entity);

        T Get(Expression<Func<T, bool>> expression);

        bool IsExist(Expression<Func<T, bool>> expression);

        IList<T> FetchAll(Expression<Func<T, bool>> expression);

        void Delete(T entity);
    }
}