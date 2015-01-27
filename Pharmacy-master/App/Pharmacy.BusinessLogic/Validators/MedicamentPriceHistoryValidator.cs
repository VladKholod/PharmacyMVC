using System.Text.RegularExpressions;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class MedicamentPriceHistoryValidator : IValidator<MedicamentPriceHistory>
    {
        private readonly IRepository<MedicamentPriceHistory> _medicamentPriceHistoryRepository;
        private readonly IRepository<Medicament> _medicamentRepository; 

        public MedicamentPriceHistoryValidator(IRepository<MedicamentPriceHistory> medicamentPriceHistoryRepository,
            IRepository<Medicament> medicamentRepository)
        {
            _medicamentPriceHistoryRepository = medicamentPriceHistoryRepository;
            _medicamentRepository = medicamentRepository;
        }

        public bool IsValidEntity(MedicamentPriceHistory entity)
        {
            return (_medicamentRepository.GetByPrimaryKey(entity.MedicamentId) != null
                    || entity.Medicament != null)
                   && Regex.IsMatch(entity.Medicament.SerialNumber, @"...-...-..")
                   && entity.Price > 0;
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 1)
                return false;

            var x = _medicamentPriceHistoryRepository.GetByPrimaryKey(keys).Id;

            return _medicamentPriceHistoryRepository.GetByPrimaryKey(keys) != null;
        }
    }
}
