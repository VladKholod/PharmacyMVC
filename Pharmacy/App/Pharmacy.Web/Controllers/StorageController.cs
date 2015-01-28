using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var storagesVM = Mapper.Map<IQueryable<Storage>, List<StorageViewModel>>(storages);

            return View(storagesVM);
        }

        [HttpGet]
        public ActionResult Details(int pharmacyId, int medicamentId)
        {
            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            var storageVM = Mapper.Map<Storage, StorageViewModel>(storage);

            return View(storageVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createStorageVM = _builder.BuildCreateStorageViewModel();
            
            return View(createStorageVM);
        }

        [HttpPost]
        public ActionResult Create(CreateStorageViewModel createStorageVM)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateStorageVM = _builder.BuildCreateStorageViewModel();
                createStorageVM.Pharmacies = tempCreateStorageVM.Pharmacies;
                createStorageVM.Medicaments = tempCreateStorageVM.Medicaments;

                return View(createStorageVM);
            }

            var storage = Mapper.Map<CreateStorageViewModel, Storage>(createStorageVM);
            _manager.Add(storage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int pharmacyId, int medicamentId)
        {
            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            var editStorageVM = Mapper.Map<Storage, EditStorageViewModel>(storage);

            return View(editStorageVM);
        }

        [HttpPost]
        public ActionResult Edit(int pharmacyId, int medicamentId, EditStorageViewModel editStorageVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editStorageVM);
            }

            var storage = _manager.GetByPrimaryKey(pharmacyId, medicamentId);
            storage.Quantity = editStorageVM.Quantity;

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
