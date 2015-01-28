using System;
using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators.Orders;

namespace Pharmacy.Web.Core.Models.Orders
{
    [Validator(typeof(EditOrderViewModelValidator))]
    public class EditOrderViewModel : CreateOrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
