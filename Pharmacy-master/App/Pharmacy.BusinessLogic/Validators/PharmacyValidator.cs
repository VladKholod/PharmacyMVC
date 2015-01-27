using System.Linq;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;

namespace Pharmacy.BusinessLogic.Validators
{
    public class PharmacyValidator : IValidator<Core.Pharmacy>
    {
        private readonly IRepository<Core.Pharmacy> _repository;

        public PharmacyValidator(IRepository<Core.Pharmacy> repository)
        {
            _repository = repository;
        }

        public bool IsValidEntity(Core.Pharmacy entity)
        {
            var entities = _repository.Find(p => p.Id != entity.Id);

            return !entities.Any()
                   || entities.All(p => p.Number != entity.Number);
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 1)
                return false;

            return _repository.GetByPrimaryKey(keys) != null;
        }
    }
}
