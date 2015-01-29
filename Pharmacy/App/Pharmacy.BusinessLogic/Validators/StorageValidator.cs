using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class StorageValidator : IValidator<Storage>
    {
        private readonly IRepository<Storage> _storageRepository;

        public StorageValidator(IRepository<Storage> storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public bool IsValidEntity(Storage entity)
        {
            return !IsEntityExist(entity.PharmacyId, entity.MedicamentId)
                   && entity.Quantity >= 0;
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 2)
                return false;

            return _storageRepository.GetByPrimaryKey(keys) != null;
        }
    }
}
