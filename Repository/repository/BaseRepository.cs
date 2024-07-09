using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class BaseRepository<T> where T:class
    {
        protected readonly DBContext Context;
        public BaseRepository(DBContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return  Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return  Context.Set<T>().Find(id);
        }

        public  IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return  Context.Set<T>().Where(predicate).ToList();
        }

        public  void Add(T entity)
        {
             Context.Set<T>().AddAsync(entity);
             Context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
             Context.Set<T>().AddRange(entities);
             Context.SaveChanges();
        }

        public  void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
             Context.SaveChanges();
        }

        public  void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
             Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
             Context.SaveChanges();
        }
    }
}
