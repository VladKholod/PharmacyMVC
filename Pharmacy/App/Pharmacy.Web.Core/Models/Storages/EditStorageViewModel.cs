﻿using FluentValidation.Attributes;
using Pharmacy.Web.Core.Validators.Storages;

namespace Pharmacy.Web.Core.Models.Storages
{
    [Validator(typeof(EditStorageViewModelValidator))]
    public class EditStorageViewModel
    {
        public int PharmacyNumber { get; set; }
        public string MedicamentName { get; set; }
        public int Quantity { get; set; }
        public int MedicamentId { get; set; }
        public int PharmacyId { get; set; }
    }
}
