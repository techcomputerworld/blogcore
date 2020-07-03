using BlogCore.AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            //include properties separadas por comas 
            if (includeProperties != null)
            {
                foreach (var includeproperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeproperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }
            return query.FirstOrDefault();

        }
        /*Este método borra el registro, yo seguramente me interese antes de borrarlo hacer una copia o poner que no 
         * aparezca en la base de datos pero si este el registro borrado introducido en la base de datos */
        public void Remove(int id)
        {
            //borra e registro que deseas borrar encontrandolo por el id
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }
        //este método tambien borra el registro en la base de datos, yo lo que quieor es no borrarlo pero que no aparezca
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
