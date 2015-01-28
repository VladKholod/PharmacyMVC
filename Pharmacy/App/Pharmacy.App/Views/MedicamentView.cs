using System;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public class MedicamentView : BaseView
    {
        private readonly IEntityManager<Medicament> _medicamentManager;
        private readonly IEntityManager<MedicamentPriceHistory> _medicamentPriceHistoryManager; 

        public MedicamentView(DataContext context)
            : base(context)
        {
            var medicamentRepository = new Repository<Medicament>(Context);
            _medicamentManager = new EntityManager<Medicament>(medicamentRepository,
                new MedicamentValidator(medicamentRepository));

            var medicamentPriceHistoryRepository = new Repository<MedicamentPriceHistory>(Context);
            _medicamentPriceHistoryManager = new EntityManager<MedicamentPriceHistory>(
                medicamentPriceHistoryRepository,
                new MedicamentPriceHistoryValidator(medicamentPriceHistoryRepository,
                    medicamentRepository));
        }

        #region CRUD
        public override void Add()
        {
            try
            {
                Console.WriteLine("Create medicament :");

                var name = EnterProperty<string>("name");
                var description = EnterProperty<string>("description");
                var price = EnterProperty<decimal>("price");
                var serialNumber = EnterProperty<string>("serialNumber(000-000-00)");

                var medicament = new Medicament()
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    SerialNumber = serialNumber
                };

                _medicamentManager.Add(medicament);
                var medicamentPriceHistory = new MedicamentPriceHistory()
                {
                    Price = medicament.Price,
                    ModifiedDate = DateTime.Now,
                    Medicament = medicament
                };
                _medicamentPriceHistoryManager.Add(medicamentPriceHistory);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Edit()
        {
            var updateMedicamentPriceHistory = false;

            var medicament = GetEntity<Medicament>();

            if (medicament == null)
                return;

            try
            {
                Console.WriteLine("Edit medicament :");

                var name = EnterProperty<string>("name");
                var description = EnterProperty<string>("description");
                var price = EnterProperty<decimal>("price");
                var serialNumber = EnterProperty<string>("serialNumber(000-000-00)");

                medicament.Name = name;
                medicament.Description = description;
                medicament.SerialNumber = serialNumber;
                if (medicament.Price != price)
                {
                    medicament.Price = price;
                    updateMedicamentPriceHistory = true;
                }
                _medicamentManager.Update(medicament);

                if (!updateMedicamentPriceHistory) 
                    return;

                var medicamentPriceHistory = new MedicamentPriceHistory()
                {
                    Price = medicament.Price,
                    ModifiedDate = DateTime.Now,
                    Medicament = medicament
                };
                _medicamentPriceHistoryManager.Add(medicamentPriceHistory);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var medicament = GetEntity<Medicament>();
            if (medicament == null)
                return;
            _medicamentManager.Remove(medicament.Id);
        }

        public override void GetByPrimaryKey()
        {
            try
            {
                var id = EnterProperty<int>("id");
                Console.WriteLine(_medicamentManager.GetByPrimaryKey(id));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
            }
        }

        public override void GetAll()
        {
            foreach (var medicament in _medicamentManager.GetAll())
            {
                Console.WriteLine(medicament);
            }
        }
        #endregion CRUD
    }
}
