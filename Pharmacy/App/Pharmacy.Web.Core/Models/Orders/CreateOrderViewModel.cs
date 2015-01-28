using System.Collections.Generic;
using System.Web.Mvc;
using Pharmacy.Core;

namespace Pharmacy.Web.Core.Models.Orders
{
    public class CreateOrderViewModel
    {
        public List<SelectListItem> Pharmacies { get; set; }
        public OperationType Type { get; set; }
        public int PharmacyId { get; set; }
    }
}
