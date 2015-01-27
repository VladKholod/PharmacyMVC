using System.Linq;
using Pharmacy.Core;

namespace Pharmacy.Contracts.Managers
{
    public interface IEntityManager<T> where T : class , IDbEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(params object[] keys);
        T GetByPrimaryKey(params object[] keys);
        IQueryable<T> GetAll();
    }
}
