using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models.MedicamentPriceHistories;

namespace Pharmacy.Web.Controllers
{
    [Authorize]
    public class MedicamentPriceHistoryController : Controller
    {
        private readonly IEntityManager<MedicamentPriceHistory> _manager;

        public MedicamentPriceHistoryController(IEntityManager<MedicamentPriceHistory> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var histories = _manager.GetAll();
            var historyViewModels = Mapper.Map<IQueryable<MedicamentPriceHistory>, List<MedicamentPriceHistoryViewModel>>(histories);
            return View(historyViewModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _manager.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
