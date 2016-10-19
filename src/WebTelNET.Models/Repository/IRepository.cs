using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebTelNET.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);

        T GetSingle(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        void Update(T entity);

        void Delete(T entity);

        bool Any(Expression<Func<T, bool>> condition);
    }
}