using System;

namespace Pharmacy.Web.Core.Models
{
    public class MedicamentPriceHistoryViewModel
    {
        public string MedicamentName { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int MedicamentId { get; set; }
    }
}
