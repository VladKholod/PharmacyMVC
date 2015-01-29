using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models.Medicaments;

namespace Pharmacy.Web.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly IMedicamentManager _manager;

        public MedicamentController(IMedicamentManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var medicaments = _manager.GetAll();
            var medicamentViewModels = Mapper.Map<IQueryable<Medicament>, List<MedicamentViewModel>>(medicaments);
            
            return View(medicamentViewModels);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var medicament = _manager.GetByPrimaryKey(id);
            var medicamentViewModel = Mapper.Map<Medicament, MedicamentViewModel>(medicament);

            return View(medicamentViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicamentViewModel medicamentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var medicament = Mapper.Map<MedicamentViewModel, Medicament>(medicamentViewModel);
            _manager.Add(medicament);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var medicament = _manager.GetByPrimaryKey(id);
            var medicamentViewModel = Mapper.Map<Medicament, MedicamentViewModel>(medicament);

            return View(medicamentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MedicamentViewModel medicamentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(medicamentViewModel);
            }

            var medicament = _manager.GetByPrimaryKey(id);
            medicament = Mapper.Map(medicamentViewModel, medicament);

            _manager.Update(medicament);

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
