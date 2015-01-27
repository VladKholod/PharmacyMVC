using System;
using System.Collections.Generic;

namespace Pharmacy.Core
{
    public class Pharmacy : BaseEntity
    {
        public string Address { get; set; }
        public int Number { get; set; }
        public string Phone { get; set; }
        public DateTime OpenDate { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Storage> Storages { get; set; }

        public Pharmacy()
        {
            Orders = new List<Order>();
            Storages = new List<Storage>();
        }

        public override string ToString()
        {
            return string.Format("Id : {0}\n\t" +
                                 "Address : {1}\n\t" +
                                 "Number : {2}\n\t" +
                                 "Phone : {3}\n\t" +
                                 "OpenDate : {4}",
                Id, Address, Number, Phone,OpenDate);
        }
    }
}
