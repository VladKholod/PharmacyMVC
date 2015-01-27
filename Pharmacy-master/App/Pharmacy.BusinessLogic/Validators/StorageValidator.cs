using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class StorageValidator : IValidator<Storage>
    {
        private readonly IRepository<Storage> _storageRepository;
        private readonly IRepository<Core.Pharmacy> _pharmacyRepository;
        private readonly IRepository<Medicament> _medicamentRepository;

        public StorageValidator(IRepository<Storage> storageRepository,
            IRepository<Core.Pharmacy> pharmacyRepository, 
            IRepository<Medicament> medicamentRepository)
        {
            _storageRepository = storageRepository;
            _pharmacyRepository = pharmacyRepository;
            _medicamentRepository = medicamentRepository;
        }

        public bool IsValidEntity(Storage entity)
        {
            return (_pharmacyRepository.GetByPrimaryKey(entity.PharmacyId) != null || entity.Pharmacy != null)
                   && (_medicamentRepository.GetByPrimaryKey(entity.MedicamentId) != null || entity.Medicament != null)
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
