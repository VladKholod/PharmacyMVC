using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Core;

namespace Pharmacy.Data
{
    public class Repository<T> : IRepository<T> where T : class, IDbEntity
    {
        private readonly DbContext _context;
        private readonly IDbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        #region CRUD
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T GetByPrimaryKey(params object[] keys)
        {
            if (typeof(T).IsSubclassOf(typeof(BaseEntity)) && keys.Length == 1)
                return _entities.Find(keys);
            
            if (keys.Length != 2)
                throw new ArgumentException();

            return _entities.Find(keys);
        }
        #endregion CRUD
    }
}
