using System.Linq;
using System.Text.RegularExpressions;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class MedicamentValidator : IValidator<Medicament>
    {
        private readonly IRepository<Medicament> _repository;

        public MedicamentValidator(IRepository<Medicament> repository)
        {
            _repository = repository;
        }

        public bool IsValidEntity(Medicament entity)
        {
            var medicaments = _repository.Find(m => m.Id != entity.Id);

            return Regex.IsMatch(entity.SerialNumber, @"^...-...-..$")
                   && medicaments.All(m => m.Name != entity.Name)
                   && entity.Price > 0;
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 1)
                return false;

            return _repository.GetByPrimaryKey(keys) != null;
        }
    }
}
