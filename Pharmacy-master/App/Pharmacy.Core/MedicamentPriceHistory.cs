using System;

namespace Pharmacy.Core
{
    public class MedicamentPriceHistory : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int MedicamentId { get; set; }

        public virtual Medicament Medicament { get; set; }

        public override string ToString()
        {
            return string.Format("Id : {0}\n\t" +
                                 "Price : {1}\n\t" +
                                 "ModifiedDate : {2}\n\t" +
                                 "MedicamentId : {3}",
                Id, Price, ModifiedDate, MedicamentId);
        }
    }
}
