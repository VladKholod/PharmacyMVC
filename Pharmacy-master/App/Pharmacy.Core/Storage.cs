namespace Pharmacy.Core
{
    public class Storage : IDbEntity
    {
        public int PharmacyId { get; set; }
        public int MedicamentId { get; set; }
        public int Quantity { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }
        public virtual Medicament Medicament { get; set; }

        public override string ToString()
        {
            return string.Format("PharmacyId : {0}\n" +
                                 "MedicamentId : {1}\n\t" +
                                 "Quantity : {2}",
                PharmacyId, MedicamentId, Quantity);
        }
    }
}
