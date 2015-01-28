namespace Pharmacy.Core
{
    public class OrderDetails : IDbEntity
    {
        public int OrderId { get; set; }
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Medicament Medicament { get; set; }

        public override string ToString()
        {
            return string.Format("OrderId : {0}\n" +
                                 "MedicamentId : {1}\n\t" +
                                 "Price : {2}\n\t" +
                                 "Quantity : {3}",
                OrderId, MedicamentId, Price, Quantity);
        }
    }
}
