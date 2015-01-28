using System;
using System.Linq;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public class MedicamentPriceHistoryView : BaseView
    {
        private readonly IEntityManager<MedicamentPriceHistory> _medicamentPriceHistoryManager;

        private readonly IRepository<MedicamentPriceHistory> _repository; 

        public MedicamentPriceHistoryView(DataContext context)
            : base(context)
        {
            var medicamentRepository = new Repository<Medicament>(Context);

            _repository = new Repository<MedicamentPriceHistory>(Context);
            _medicamentPriceHistoryManager = new EntityManager<MedicamentPriceHistory>(
                _repository, new MedicamentPriceHistoryValidator(_repository,
                    medicamentRepository));
        }

        #region CRUD
        public override void Add()
        {
        }

        public override void Edit()
        {
            var medicamentPriceHistory = GetEntity<MedicamentPriceHistory>();

            if(medicamentPriceHistory==null)
                return;

            try
            {
                Console.WriteLine("Edit medicament :");

                var modifiedDate = EnterProperty<DateTime>("modifiedDate");

                medicamentPriceHistory.ModifiedDate = modifiedDate;
                _medicamentPriceHistoryManager.Update(medicamentPriceHistory);

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var medicamentPriceHistory = GetEntity<MedicamentPriceHistory>();
            if(medicamentPriceHistory!=null)
                _medicamentPriceHistoryManager.Remove(medicamentPriceHistory.Id);
        }

        public override void GetByPrimaryKey()
        {
            try
            {
                var id = EnterProperty<int>("medicamentId");

                var medicamentPriceHistories = _repository.Find(mph => mph.MedicamentId == id).ToList();
                if (medicamentPriceHistories.Count == 0) 
                    return;

                foreach (var item in medicamentPriceHistories)
                    Console.WriteLine(item);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
            }
        }

        public override void GetAll()
        {
            foreach (var medicamentPriceHistory in _medicamentPriceHistoryManager.GetAll())
            {
                Console.WriteLine(medicamentPriceHistory);
            }
        }
        #endregion CRUD
    }
}
