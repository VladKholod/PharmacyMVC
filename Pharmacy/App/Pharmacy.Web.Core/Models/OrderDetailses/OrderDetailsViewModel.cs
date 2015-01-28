using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators.OrderDetailses;

namespace Pharmacy.Web.Core.Models.OrderDetailses
{
    [Validator(typeof(OrderDetailsViewModelValidator))]
    public class OrderDetailsViewModel
    {
        public string MedicamentName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int MedicamentId { get; set; }
    }
}
