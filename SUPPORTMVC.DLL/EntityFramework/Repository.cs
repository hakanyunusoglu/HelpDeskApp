using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SUPPORTMVC.CORE.DataAccess;

namespace SUPPORTMVC.DLL.EntityFramework
{
    public class Repository<T>: RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objSet;

        public Repository()
        {
            _objSet = db.Set<T>();
        }
       
        public List<T> List()
        {
            return _objSet.ToList();
;       }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objSet.Where(where).ToList();
        }

        public IQueryable FilterList(Expression<Func<T, bool>> where)
        {
            return _objSet.Where(where);
        }
        public IQueryable ListQueryable()
        {
            return _objSet.AsQueryable<T>();
;        }

        public int Insert(T obj)
        {
            _objSet.Add(obj);
          return  db.SaveChanges();
        }

        public int Update(T obj)
        {
          return db.SaveChanges();
        }

        public int Delete(T obj)
        {
            _objSet.Remove(obj);
            return db.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objSet.FirstOrDefault(where);
        }

    }
}