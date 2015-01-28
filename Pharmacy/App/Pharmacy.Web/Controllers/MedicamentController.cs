using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models;
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
            var medicamentsVM = Mapper.Map<IQueryable<Medicament>, List<MedicamentViewModel>>(medicaments);
            
            return View(medicamentsVM);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var medicament = _manager.GetByPrimaryKey(id);
            var medicamentVM = Mapper.Map<Medicament, MedicamentViewModel>(medicament);

            return View(medicamentVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MedicamentViewModel medicamentVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var medicament = Mapper.Map<MedicamentViewModel, Medicament>(medicamentVM);
            _manager.Add(medicament);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var medicament = _manager.GetByPrimaryKey(id);
            var medicamentVM = Mapper.Map<Medicament, MedicamentViewModel>(medicament);

            return View(medicamentVM);
        }

        [HttpPost]
        public ActionResult Edit(int id, MedicamentViewModel medicamentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(medicamentVM);
            }

            var medicament = Mapper.Map<MedicamentViewModel, Medicament>(medicamentVM);
            

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
