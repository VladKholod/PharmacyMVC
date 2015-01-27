using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators;

namespace Pharmacy.Web.Core.Models
{
    [Validator(typeof(MedicamentViewModelValidator))]
    public class MedicamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
