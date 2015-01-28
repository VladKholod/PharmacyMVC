using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var orderDetailsVM = Mapper.Map<IQueryable<OrderDetails>, List<OrderDetailsViewModel>>(orderDetails);

            return View(orderDetailsVM);
        }

        [HttpGet]
        public ActionResult Details(int orderId, int medicamentId)
        {
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            var orderDetailsVM = Mapper.Map<OrderDetails, OrderDetailsViewModel>(orderDetails);

            return View(orderDetailsVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createOrderDetailsVM = _builder.BuildCreateOrderDetailsViewModel();

            return View(createOrderDetailsVM);
        }

        [HttpPost]
        public ActionResult Create(CreateOrderDetailsViewModel createOrderDetailsVM)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateOrderDetailsVM = _builder.BuildCreateOrderDetailsViewModel();
                createOrderDetailsVM.Orders = tempCreateOrderDetailsVM.Orders;
                createOrderDetailsVM.Medicaments = tempCreateOrderDetailsVM.Medicaments;

                return View(createOrderDetailsVM);
            }

            var orderDetails = Mapper.Map<CreateOrderDetailsViewModel, OrderDetails>(createOrderDetailsVM);
            _manager.Add(orderDetails);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int orderId, int medicamentId)
        {
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            var editOrderDetailsVM = Mapper.Map<OrderDetails, EditOrderDetailsViewModel>(orderDetails);

            return View(editOrderDetailsVM);
        }

        [HttpPost]
        public ActionResult Edit(int orderId,int medicamentId, EditOrderDetailsViewModel editOrderDetailsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editOrderDetailsVM);
            }
            
            var orderDetails = _manager.GetByPrimaryKey(orderId, medicamentId);
            orderDetails.Price = editOrderDetailsVM.Price;
            orderDetails.Quantity = editOrderDetailsVM.Quantity;

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
                Price = medicament.Price
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
