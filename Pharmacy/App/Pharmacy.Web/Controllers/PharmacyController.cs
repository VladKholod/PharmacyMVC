using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Web.Core.Models.Pharmacies;

namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var pharmacyViewModels = Mapper.Map<IQueryable<Pharmacy.Core.Pharmacy>, List<PharmacyViewModel>>(pharmacies);

            return View(pharmacyViewModels);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var pharmacy = _manager.GetByPrimaryKey(id);
            var pharmacyViewModel = Mapper.Map<Pharmacy.Core.Pharmacy, PharmacyViewModel>(pharmacy);

            return View(pharmacyViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PharmacyViewModel pharmacyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var pharmacy = Mapper.Map<PharmacyViewModel, Pharmacy.Core.Pharmacy>(pharmacyViewModel);
            _manager.Add(pharmacy);

            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pharmacy = _manager.GetByPrimaryKey(id);
            var pharmacyViewModel = Mapper.Map<Pharmacy.Core.Pharmacy, PharmacyViewModel>(pharmacy);

            return View(pharmacyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PharmacyViewModel pharmacyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pharmacyViewModel);
            }

            var pharmacy = _manager.GetByPrimaryKey(id);
            pharmacy = Mapper.Map(pharmacyViewModel,pharmacy);
            
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
