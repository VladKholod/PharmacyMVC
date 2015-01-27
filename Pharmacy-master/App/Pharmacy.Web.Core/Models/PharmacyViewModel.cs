using System;
using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators;

namespace Pharmacy.Web.Core.Models
{
    [Validator(typeof(PharmacyViewModelValidator))]
    public class PharmacyViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Phone { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
