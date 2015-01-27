using System.Linq;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Managers
{
    public class EntityManager<T> : IEntityManager<T> where T : class, IDbEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IValidator<T> _validatorBehaviour; 

        public EntityManager(IRepository<T> repository, IValidator<T> validatorBehaviour)
        {
            _repository = repository;
            _validatorBehaviour = validatorBehaviour;
        }

        #region CRUD
        public virtual void Add(T entity)
        {
            if (!_validatorBehaviour.IsValidEntity(entity)) 
                return;
            
            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if(_validatorBehaviour.IsValidEntity(entity))
                _repository.SaveChanges();
        }

        public virtual void Remove(params object[] keys)
        {
            if (!_validatorBehaviour.IsEntityExist(keys)) 
                return;
            
            _repository.Remove(_repository.GetByPrimaryKey(keys));
            _repository.SaveChanges();
        }

        public virtual T GetByPrimaryKey(params object[] keys)
        {
            return _validatorBehaviour.IsEntityExist(keys) ? _repository.GetByPrimaryKey(keys) : null;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion CRUD
    }
}
