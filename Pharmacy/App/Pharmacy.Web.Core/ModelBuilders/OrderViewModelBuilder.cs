using System.Collections.Generic;
using System.Web.Mvc;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models.Orders;

namespace Pharmacy.Web.Core.ModelBuilders
{
    public class OrderViewModelBuilder
    {
        private readonly IEntityManager<Order> _orderManager;
        private readonly IEntityManager<Pharmacy.Core.Pharmacy> _pharmacyManager;

        public OrderViewModelBuilder(IEntityManager<Order> orderManager, IEntityManager<Pharmacy.Core.Pharmacy> pharmacyManager)
        {
            _orderManager = orderManager;
            _pharmacyManager = pharmacyManager;
        }

        public CreateOrderViewModel BuildCreateOrderViewModel()
        {
            var model = new CreateOrderViewModel
            {
                Pharmacies = new List<SelectListItem>()
            };

            foreach (var pharmacy in _pharmacyManager.GetAll())
            {
                model.Pharmacies.Add(new SelectListItem()
                {
                    Value = pharmacy.Id.ToString(),
                    Text = pharmacy.Number.ToString()
                });
            }

            model.Type = OperationType.Purchase;

            return model;
        }

        public EditOrderViewModel BuildEditOrderViewModel(int id)
        {
            var createModel = BuildCreateOrderViewModel();
            var order = _orderManager.GetByPrimaryKey(id);

            return order == null
                ? null
                : new EditOrderViewModel()
                {
                    Id = order.Id,
                    Type = order.Type,
                    PharmacyId = order.PharmacyId,
                    Pharmacies = createModel.Pharmacies,
                    Date = order.Date
                };
        }
    }
}
