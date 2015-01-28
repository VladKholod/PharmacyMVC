using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Pharmacy.Web.Core.Models.OrderDetailses;

namespace Pharmacy.Web.Core.Validators.OrderDetailses
{
    
    public class EditOrderDetailsViewModelValidator : AbstractValidator<EditOrderDetailsViewModel>
    {
        public EditOrderDetailsViewModelValidator()
        {
            RuleFor(od => od.Price)
                .NotNull().WithMessage("Price must be filled")
                .GreaterThanOrEqualTo(1).WithMessage("Price must be greater than 0");
            RuleFor(od => od.Quantity)
                .NotNull().WithMessage("Quantity must be filled")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than 0");
        }
    }
}
