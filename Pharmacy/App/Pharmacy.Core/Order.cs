using System;
using System.Collections.Generic;

namespace Pharmacy.Core
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public OperationType Type { get; set; }
        public int PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public virtual List<OrderDetails> OrderDetailses { get; set; }
        
        public Order()
        {
            OrderDetailses = new List<OrderDetails>();
        }

        public override string ToString()
        {
            return string.Format("Id : {0}\n\t" +
                                 "Date : {1}\n\t" +
                                 "Type : {2}\n\t" +
                                 "PharmacyId : {3}",
                Id, Date, Type, PharmacyId);
        }
    }
}
