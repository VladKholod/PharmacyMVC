using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models.OrderDetailses;

namespace Pharmacy.Web.Core.ModelBuilders
{
    public class OrderDetailsViewModelBuilder
    {
        private readonly IEntityManager<OrderDetails> _orderDetailsManager;
        private readonly IEntityManager<Order> _orderManager;
        private readonly IMedicamentManager _medicamentManager;

        public OrderDetailsViewModelBuilder(IEntityManager<OrderDetails> orderDetailsManager, IEntityManager<Order> orderManager, IMedicamentManager medicamentManager)
        {
            _orderDetailsManager = orderDetailsManager;
            _orderManager = orderManager;
            _medicamentManager = medicamentManager;
        }

        public CreateOrderDetailsViewModel BuildCreateOrderDetailsViewModel()
        {
            var model = new CreateOrderDetailsViewModel()
            {
                Orders = new List<SelectListItem>(),
                Medicaments = new List<SelectListItem>()
            };

            foreach (var order in _orderManager.GetAll())
            {
                model.Orders.Add(new SelectListItem()
                {
                    Value = order.Id.ToString(),
                    Text = order.Id.ToString()
                });
            }

            foreach (var medicament in _medicamentManager.GetAll())
            {
                model.Medicaments.Add(new SelectListItem()
                {
                    Value = medicament.Id.ToString(),
                    Text = medicament.Name
                }); 
            }

            return model;
        }
    }
}
