using System.Linq;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Managers
{
    public class EntityManager<T> : IEntityManager<T> where T : class, IDbEntity
    {
        protected readonly IRepository<T> Repository;
        protected readonly IValidator<T> ValidatorBehaviour; 

        public EntityManager(IRepository<T> repository, IValidator<T> validatorBehaviour)
        {
            Repository = repository;
            ValidatorBehaviour = validatorBehaviour;
        }

        #region CRUD
        public virtual void Add(T entity)
        {
            if (!ValidatorBehaviour.IsValidEntity(entity)) 
                return;
            
            Repository.Add(entity);
            Repository.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if(ValidatorBehaviour.IsValidEntity(entity))
                Repository.SaveChanges();
        }

        public virtual void Remove(params object[] keys)
        {
            if (!ValidatorBehaviour.IsEntityExist(keys)) 
                return;
            
            Repository.Remove(Repository.GetByPrimaryKey(keys));
            Repository.SaveChanges();
        }

        public virtual T GetByPrimaryKey(params object[] keys)
        {
            return ValidatorBehaviour.IsEntityExist(keys) ? Repository.GetByPrimaryKey(keys) : null;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Repository.GetAll();
        }
        #endregion CRUD
    }
}