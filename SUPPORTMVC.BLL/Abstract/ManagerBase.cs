using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SUPPORTMVC.CORE.DataAccess;
using SUPPORTMVC.DLL.EntityFramework;

namespace SUPPORTMVC.BLL.Abstract
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> @where)
        {
            return repo.List(where);
        }

        public IQueryable FilterList(Expression<Func<T, bool>> @where)
        {
            return repo.FilterList(where);
        }

        public IQueryable ListQueryable()
        {
          return repo.ListQueryable();
        } 

        public int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }

        public int Delete(T obj)
        {
           return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> @where)
        {
            return repo.Find(where);
        }
    }
}