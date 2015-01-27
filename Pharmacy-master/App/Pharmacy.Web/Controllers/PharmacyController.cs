using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Web.Core.Models;

namespace Pharmacy.Web.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly IEntityManager<Pharmacy.Core.Pharmacy> _manager; 

        public PharmacyController(IEntityManager<Pharmacy.Core.Pharmacy> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var pharmacies = _manager.GetAll();
            var pharmaciesVM = Mapper.Map<IQueryable<Pharmacy.Core.Pharmacy>, List<PharmacyViewModel>>(pharmacies);

            return View(pharmaciesVM);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var pharmacy = _manager.GetByPrimaryKey(id);
            var pharmacyVM = Mapper.Map<Pharmacy.Core.Pharmacy, PharmacyViewModel>(pharmacy);

            return View(pharmacyVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PharmacyViewModel pharmacyVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var pharmacy = Mapper.Map<PharmacyViewModel, Pharmacy.Core.Pharmacy>(pharmacyVM);
            _manager.Add(pharmacy);

            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pharmacy = _manager.GetByPrimaryKey(id);
            var pharmacyVM = Mapper.Map<Pharmacy.Core.Pharmacy, PharmacyViewModel>(pharmacy);

            return View(pharmacyVM);
        }

        [HttpPost]
        public ActionResult Edit(int id, PharmacyViewModel pharmacyVM)
        {
            if (!ModelState.IsValid)
            {
                return View(pharmacyVM);
            }

            var tempPharmacy = Mapper.Map<PharmacyViewModel, Pharmacy.Core.Pharmacy>(pharmacyVM);
            
            var pharmacy = _manager.GetByPrimaryKey(tempPharmacy.Id);
            pharmacy.Number = tempPharmacy.Number;
            pharmacy.OpenDate = tempPharmacy.OpenDate;
            pharmacy.Phone = tempPharmacy.Phone;
            pharmacy.Address = tempPharmacy.Address;

            _manager.Update(pharmacy);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _manager.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
