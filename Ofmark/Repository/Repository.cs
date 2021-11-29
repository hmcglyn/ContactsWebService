using Microsoft.EntityFrameworkCore;
using Ofmark.Entities;
using Ofmark.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ofmark.Repository
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        private DbSet<T> _objSet;
        private DateTime _now;
        public Repository()
        {
            _objSet = db.Set<T>();
            _now = DateTime.Now;
        }
        public bool Create(T entity)
        {
            try
            {
                _objSet.Add(entity);
                if (entity is BaseEntity)
                {
                    BaseEntity o = entity as BaseEntity;
                    o.Created = _now;
                    o.LastUpdated=_now;
                }                
                return Save();
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public bool Delete(T entity)
        {
            _objSet.Update(entity);
            if (entity is BaseEntity)
            {
                BaseEntity o = entity as BaseEntity;
                o.Deleted = _now;
            }
            return Save();
        }
        public T Find(Expression<Func<T, bool>> expression)
        {
            return _objSet.FirstOrDefault(expression);
        }      
        public List<T> List()
        {
            return _objSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return _objSet.Where(expression).ToList();
        }

        public bool Save()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        public bool Update(T entity)
        {
            try
            {
                _objSet.Update(entity);
                if (entity is BaseEntity)
                {
                    BaseEntity o = entity as BaseEntity;
                    o.LastUpdated = _now;
                }
                return Save();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
