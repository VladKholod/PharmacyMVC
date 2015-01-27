using System;
using System.Linq;
using System.Linq.Expressions;
using Pharmacy.Core;

namespace Pharmacy.Contracts.Repositories
{
    public interface IRepository<T> where T : class, IDbEntity
    {
        void Add(T entity);
        void Remove(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void SaveChanges();
        T GetByPrimaryKey(params object[] keys);
    }
}
