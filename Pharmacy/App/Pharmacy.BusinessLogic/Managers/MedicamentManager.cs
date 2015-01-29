using System;
using System.Linq;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Managers
{
    public class MedicamentManager: IEntityManager<Medicament>, IMedicamentManager
    {

        private readonly IRepository<Medicament> _medicamentRepository; 
        private readonly IRepository<MedicamentPriceHistory> _medicamentPriceHistoryRepository;
        private readonly IValidator<Medicament> _medicamentValidator; 

        public MedicamentManager(IRepository<Medicament> medicamentRepository, IRepository<MedicamentPriceHistory> medicamentPriceHistoryRepository, IValidator<Medicament> medicamentValidator)
        {
            _medicamentRepository = medicamentRepository;
            _medicamentPriceHistoryRepository = medicamentPriceHistoryRepository;
            
            _medicamentValidator = medicamentValidator;
        }

        public void Add(Medicament entity)
        {
            if (!_medicamentValidator.IsValidEntity(entity))
                return;

            _medicamentRepository.Add(entity);
            _medicamentRepository.SaveChanges();

            CreateMedicamentPriceHistory(entity);
        }

        public void Update(Medicament entity)
        {
            if (!_medicamentValidator.IsValidEntity(entity))
                return;

            var medicament = _medicamentRepository.GetByPrimaryKey(entity.Id);

            if (medicament.Price != entity.Price)
            {
                medicament.MedicamentPriceHistories.Add(new MedicamentPriceHistory()
                {
                    Price = medicament.Price,
                    ModifiedDate = DateTime.Now,
                    MedicamentId = medicament.Id
                });
            }

            medicament.Description = entity.Description;
            medicament.Name = entity.Name;
            medicament.SerialNumber = entity.SerialNumber;
            medicament.Price = entity.Price;

            _medicamentRepository.SaveChanges();
        }

        public void Remove(params object[] keys)
        {
            if (!_medicamentValidator.IsEntityExist(keys))
                return;

            _medicamentRepository.Remove(_medicamentRepository.GetByPrimaryKey(keys));
            _medicamentRepository.SaveChanges();
        }

        public Medicament GetByPrimaryKey(params object[] keys)
        {
            return _medicamentValidator.IsEntityExist(keys) ? _medicamentRepository.GetByPrimaryKey(keys) : null;
        }

        public IQueryable<Medicament> GetAll()
        {
            return _medicamentRepository.GetAll();
        }

        private void CreateMedicamentPriceHistory(Medicament entity)
        {
            var medicamentPriceHistory =  new MedicamentPriceHistory()
            {
                Medicament = entity,
                ModifiedDate = DateTime.Now,
                Price = entity.Price
            };

            _medicamentPriceHistoryRepository.Add(medicamentPriceHistory);
            _medicamentPriceHistoryRepository.SaveChanges();
        }
    }
}
