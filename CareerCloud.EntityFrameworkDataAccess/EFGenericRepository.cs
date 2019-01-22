using CareerCloud.DataAccessLayer;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {

        public JOB_PORTAL_DBEntities _context;
        public EFGenericRepository(bool createProxy = true)
        //public EFGenericRepository()
        {
           // _context = new JOB_PORTAL_DBEntities(createProxy);
            _context = new JOB_PORTAL_DBEntities();

        }

        public void Add(params T[] items)
        {
            foreach(T item in items)
            {
                _context.Entry(item).State = System.Data.Entity.EntityState.Added;
            }
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach(Expression<Func<T, object>> navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (Expression<Func<T, object>> navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (Expression<Func<T, object>> navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            //return dbQuery.Where(where).FirstOrDefault();
            return dbQuery.FirstOrDefault(where);
        }

        public void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach(T item in items)
            {
                _context.Entry(item).State = System.Data.Entity.EntityState.Modified;

            }
            _context.SaveChanges();
        }
    }
   
}
