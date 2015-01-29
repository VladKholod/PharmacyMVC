using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.ModelBuilders;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web.Controllers
{
    public class StorageController : Controller
    {
        private readonly IEntityManager<Storage> _manager;
        private readonly StorageViewModelBuilder _builder;

        public StorageController(IEntityManager<Storage> manager, StorageViewModelBuilder builder)
        {
            _manager = manager;
            _builder = builder;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var storages = _manager.GetAll();
            var storageViewModels = Mapper.Map<IQueryable<Storage>, List<StorageViewModel>>(storages);

            return View(storageViewModels);
        }

        [HttpGet]
        public ActionResult Details(int pharmacyId, int medicamentId)
        {
            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            var storageViewModel = Mapper.Map<Storage, StorageViewModel>(storage);

            return View(storageViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createStorageViewModel = _builder.BuildCreateStorageViewModel();
            
            return View(createStorageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStorageViewModel createStorageViewModel)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateStorageViewModel = _builder.BuildCreateStorageViewModel();
                createStorageViewModel.Pharmacies = tempCreateStorageViewModel.Pharmacies;
                createStorageViewModel.Medicaments = tempCreateStorageViewModel.Medicaments;

                return View(createStorageViewModel);
            }

            var storage = Mapper.Map<CreateStorageViewModel, Storage>(createStorageViewModel);
            _manager.Add(storage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int pharmacyId, int medicamentId)
        {
            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            var editStorageViewModel = Mapper.Map<Storage, EditStorageViewModel>(storage);

            return View(editStorageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int pharmacyId, int medicamentId, EditStorageViewModel editStorageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editStorageViewModel);
            }

            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            storage.Quantity = editStorageViewModel.Quantity;

            _manager.Update(storage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int pharmacyId, int medicamentId)
        {
            _manager.Remove(pharmacyId, medicamentId);

            return RedirectToAction("Index");
        }
    }
}
