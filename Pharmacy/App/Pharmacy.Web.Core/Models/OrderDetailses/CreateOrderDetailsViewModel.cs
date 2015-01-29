using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators.OrderDetailses;

namespace Pharmacy.Web.Core.Models.OrderDetailses
{
    [Validator(typeof(CreateOrderDetailsViewModelValidator))]
    public class CreateOrderDetailsViewModel
    {
        public List<SelectListItem> Orders { get; set; }
        public List<SelectListItem> Medicaments { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int MedicamentId { get; set; }
    }
}
