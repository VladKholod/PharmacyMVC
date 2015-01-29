using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.ModelBuilders;
using Pharmacy.Web.Core.Models.OrderDetailses;

namespace Pharmacy.Web.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IEntityManager<OrderDetails> _manager;
        private readonly OrderDetailsViewModelBuilder _builder;
        private readonly IMedicamentManager _medicamentManager;

        public OrderDetailsController(IEntityManager<OrderDetails> manager, IMedicamentManager medicamentManager, OrderDetailsViewModelBuilder builder)
        {
            _manager = manager;
            _builder = builder;
            _medicamentManager = medicamentManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var orderDetails = _manager.GetAll();
            var orderDetailsViewModels = Mapper.Map<IQueryable<OrderDetails>, List<OrderDetailsViewModel>>(orderDetails);

            return View(orderDetailsViewModels);
        }

        [HttpGet]
        public ActionResult Details(int orderId, int medicamentId)
        {
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            var orderDetailsViewModel = Mapper.Map<OrderDetails, OrderDetailsViewModel>(orderDetails);

            return View(orderDetailsViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createOrderDetailsViewModel = _builder.BuildCreateOrderDetailsViewModel();

            return View(createOrderDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderDetailsViewModel createOrderDetailsViewModel)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateOrderDetailsViewModel = _builder.BuildCreateOrderDetailsViewModel();
                createOrderDetailsViewModel.Orders = tempCreateOrderDetailsViewModel.Orders;
                createOrderDetailsViewModel.Medicaments = tempCreateOrderDetailsViewModel.Medicaments;

                return View(createOrderDetailsViewModel);
            }

            var orderDetails = Mapper.Map<CreateOrderDetailsViewModel, OrderDetails>(createOrderDetailsViewModel);
            _manager.Add(orderDetails);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int orderId, int medicamentId)
        {
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            var editOrderDetailsViewModel = Mapper.Map<OrderDetails, EditOrderDetailsViewModel>(orderDetails);

            return View(editOrderDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int orderId,int medicamentId, EditOrderDetailsViewModel editOrderDetailsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editOrderDetailsViewModel);
            }
            
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            orderDetails.Price = editOrderDetailsViewModel.Price;
            orderDetails.Quantity = editOrderDetailsViewModel.Quantity;

            _manager.Update(orderDetails);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int orderId, int medicamentId)
        {
            _manager.Remove(orderId, medicamentId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetPrice(int medicamentId)
        {
            var medicament = _medicamentManager.GetByPrimaryKey(medicamentId);
            return Json(new
            {
                medicament.Price
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
