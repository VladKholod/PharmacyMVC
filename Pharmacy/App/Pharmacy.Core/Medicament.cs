using System.Collections.Generic;

namespace Pharmacy.Core
{
    public class Medicament : BaseEntity
    {
        public string Name { get; set; }
        public string SerialNumber{ get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual List<Storage> Storages { get; set; }
        public virtual List<MedicamentPriceHistory> MedicamentPriceHistories { get; set; }
        public virtual List<OrderDetails> OrderDetailses { get; set; }

        public Medicament()
        {
            Storages = new List<Storage>();
            MedicamentPriceHistories = new List<MedicamentPriceHistory>();
            OrderDetailses = new List<OrderDetails>();
        }

        public override string ToString()
        {
            return string.Format("Id : {0}\n\t" +
                                 "Name : {1}\n\t" +
                                 "SerialNumber : {2}\n\t" +
                                 "Description : {3}\n\t" +
                                 "Price : {4}",
                Id, Name, SerialNumber, Description, Price);
        }
    }
}
