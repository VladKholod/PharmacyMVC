using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.ModelBuilders;
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
            var orderViewModels = Mapper.Map<IQueryable<Order>, List<OrderViewModel>>(orders);

            return View(orderViewModels);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = _manager.GetByPrimaryKey(id);
            var orderViewModel = Mapper.Map<Order, OrderViewModel>(order);

            return View(orderViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createOrderViewModel = _builder.BuildCreateOrderViewModel();

            return View(createOrderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel createOrderViewModel)
        {
            if (!ModelState.IsValid)
            {
                var tempCreateOrderViewModel = _builder.BuildCreateOrderViewModel();
                createOrderViewModel.Pharmacies = tempCreateOrderViewModel.Pharmacies;
                return View(createOrderViewModel);
            }

            var order = Mapper.Map<CreateOrderViewModel, Order>(createOrderViewModel);
            order.Date = DateTime.Now;

            _manager.Add(order);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editOrderViewModel = _builder.BuildEditOrderViewModel(id);

            return View(editOrderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditOrderViewModel editOrderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editOrderViewModel);
            }

            var order = _manager.GetByPrimaryKey(id);
            order = Mapper.Map(editOrderViewModel,order);

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
