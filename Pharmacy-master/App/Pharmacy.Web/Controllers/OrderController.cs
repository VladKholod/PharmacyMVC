using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models;

namespace Pharmacy.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEntityManager<Order> _manager;

        public OrderController(IEntityManager<Order> manager)
        {
            _manager = manager;
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
