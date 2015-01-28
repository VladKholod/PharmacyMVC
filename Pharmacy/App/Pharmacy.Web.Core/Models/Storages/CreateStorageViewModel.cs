using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators.Storages;

namespace Pharmacy.Web.Core.Models.Storages
{
    [Validator(typeof(CreateStorageViewModelValidator))]
    public class CreateStorageViewModel
    {
        public List<SelectListItem> Pharmacies { get; set; }
        public List<SelectListItem> Medicaments { get; set; }
        public int Quantity { get; set; }
        public int PharmacyId { get; set; }
        public int MedicamentId { get; set; }
    }
}
