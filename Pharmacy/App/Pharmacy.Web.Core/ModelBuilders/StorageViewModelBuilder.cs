using System.Collections.Generic;
using System.Web.Mvc;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web.Core.ModelBuilders
{
    public class StorageViewModelBuilder
    {
        private readonly IEntityManager<Storage> _storageManager;
        private readonly IEntityManager<Pharmacy.Core.Pharmacy> _pharmacyManager;
        private readonly IMedicamentManager _medicamentManager;

        public StorageViewModelBuilder(IEntityManager<Storage> storageManager, IEntityManager<Pharmacy.Core.Pharmacy> pharmacyManager, IMedicamentManager medicamentManager)
        {
            _storageManager = storageManager;
            _pharmacyManager = pharmacyManager;
            _medicamentManager = medicamentManager;
        }

        public CreateStorageViewModel BuildCreateStorageViewModel()
        {
            var model = new CreateStorageViewModel()
            {
                Pharmacies = new List<SelectListItem>(),
                Medicaments = new List<SelectListItem>()
            };

            foreach (var pharmacy in _pharmacyManager.GetAll())
            {
                model.Pharmacies.Add(new SelectListItem()
                {
                    Value = pharmacy.Id.ToString(),
                    Text = pharmacy.Number.ToString()
                });
            }

            foreach (var medicament in _medicamentManager.GetAll())
            {
                model.Medicaments.Add(new SelectListItem()
                {
                    Value = medicament.Id.ToString(),
                    Text = medicament.Name
                });
            }

            return model;
        }
    }
}
