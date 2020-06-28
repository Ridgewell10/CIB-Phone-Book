using Contracts;
using Infustructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Respository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CIBDbContext RepositoryContext { get; set; }

        public RepositoryBase(CIBDbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            //if(this.RepositoryContext.)

        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}
