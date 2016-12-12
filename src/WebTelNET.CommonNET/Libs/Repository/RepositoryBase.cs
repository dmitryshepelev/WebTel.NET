using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebTelNET.CommonNET.Libs.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; }

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public virtual T Create(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = Context.Set<T>();
            return predicate != null ? Context.Set<T>().Where(predicate) : query;
        }

        public virtual T Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
            }
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();

        }

        public virtual bool Any(Expression<Func<T, bool>> condition)
        {
            return Context.Set<T>().Any(condition);
        }
    }
}