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
    public class StorageView : BaseView
    {
        private readonly IEntityManager<Storage> _manager;
        
        private readonly IRepository<Storage> _repository; 

        public StorageView(DataContext context)
            : base(context)
        {
            _repository = new Repository<Storage>(Context);
            var medicamentRepository = new Repository<Medicament>(Context);
            var pharmacyRepository = new Repository<Core.Pharmacy>(Context);

            _manager = new EntityManager<Storage>(_repository,
                new StorageValidator(_repository, pharmacyRepository, medicamentRepository));
        }

        #region CRUD
        public override void Add()
        {
            try
            {
                Console.WriteLine("Create storage :");

                var pharmacy = GetEntity<Core.Pharmacy>();
                if (pharmacy == null)
                    return;
                
                var medicament = GetEntity<Medicament>();
                if (medicament == null)
                    return;

                var quantity = EnterProperty<int>("quantity");

                var storage = new Storage()
                {
                    Pharmacy = pharmacy,
                    Medicament = medicament,
                    Quantity = quantity
                };

                _manager.Add(storage);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Edit()
        {
            var storage = GetEntity<Storage>();

            if (storage == null)
                return;

            try
            {
                Console.WriteLine("Edit storage :");

                var pharmacy = GetEntity<Core.Pharmacy>();
                if (pharmacy == null)
                    return;

                var medicament = GetEntity<Medicament>();
                if (medicament == null)
                    return;

                var quantity = EnterProperty<int>("quantity");

                storage.Pharmacy = pharmacy;
                storage.Medicament = medicament;
                storage.Quantity = quantity;

                _manager.Update(storage);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var storage = GetEntity<Storage>();
            
            if (storage == null)
                return;

            _manager.Remove(storage.PharmacyId, storage.MedicamentId);
        }

        public override void GetByPrimaryKey()
        {
            try
            {
                var id = EnterProperty<int>("pharmacyId");

                var storages = _repository.Find(s => s.PharmacyId == id).ToList();
                if (storages.Count == 0)
                    return;

                foreach (var item in storages)
                    Console.WriteLine(item);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
            }
        }

        public override void GetAll()
        {
            foreach (var storage in _manager.GetAll())
            {
                Console.WriteLine(storage);
            }
        }
        #endregion CRUD
    }
}
