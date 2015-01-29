using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.ModelBuilders;
using Pharmacy.Web.Core.Models;
using Pharmacy.Web.Core.Models.Orders;

namespace Pharmacy.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEntityManager<Order> _manager;
        private readonly OrderViewModelBuilder _builder;

        public OrderController(IEntityManager<Order> manager, OrderViewModelBuilder builder)
        {
            _manager = manager;
            _builder = builder;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var orders = _manager.GetAll();
            var ordersVM = Mapper.Map<IQueryable<Order>, List<OrderViewModel>>(orders);

            return View(ordersVM);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = _manager.GetByPrimaryKey(id);
            var orderVM = Mapper.Map<Order, OrderViewModel>(order);

            return View(orderVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createOrderVM = _builder.BuildCreateOrderViewModel();

            return View(createOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel createOrderVM)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateOrderVM = _builder.BuildCreateOrderViewModel();
                createOrderVM.Pharmacies = tempCreateOrderVM.Pharmacies;
                return View(createOrderVM);
            }

            var order = Mapper.Map<CreateOrderViewModel, Order>(createOrderVM);
            order.Date = DateTime.Now;

            _manager.Add(order);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editOrderVM = _builder.BuildEditOrderViewModel(id);

            return View(editOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditOrderViewModel editOrderVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editOrderVM);
            }

            var order = _manager.GetByPrimaryKey(id);
            order = Mapper.Map(editOrderVM,order);

            _manager.Update(order);

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
