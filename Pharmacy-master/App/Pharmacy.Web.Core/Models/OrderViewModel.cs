using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Pharmacy.Core;
using Pharmacy.Web.Core.Validators;

namespace Pharmacy.Web.Core.Models
{
    [Validator(typeof(OrderViewModelValidator))]
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OperationType Type { get; set; }
        public int PharmacyId { get; set; }
        public int PharmacyNumber { get; set; }
        public List<OrderDetails> OrderDetailses { get; set; }
    }
}
