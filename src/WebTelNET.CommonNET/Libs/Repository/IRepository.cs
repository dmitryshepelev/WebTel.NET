using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebTelNET.CommonNET.Libs.Repository
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingleWithNavigationProperties(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> GetAllWithNavigationProperties(Expression<Func<T, bool>> predicate = null);

        T Update(T entity);

        void Delete(T entity);

        bool Any(Expression<Func<T, bool>> condition);
    }
}