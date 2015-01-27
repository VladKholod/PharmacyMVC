using System.Data.Entity.ModelConfiguration;
using Pharmacy.Core;

namespace Pharmacy.Data.Mappings
{
    public class MedicamentPriceHistoryMap : EntityTypeConfiguration<MedicamentPriceHistory>
    {
        public MedicamentPriceHistoryMap()
        {
            this.HasKey(mph => mph.Id);

            this.Property(mph => mph.Price)
                .IsRequired();
            this.Property(mph => mph.ModifiedDate)
                .IsRequired();
            this.Property(mph => mph.MedicamentId)
                .IsRequired();

            this.HasRequired<Medicament>(mph=>mph.Medicament)
                .WithMany(m=>m.MedicamentPriceHistories)
                .HasForeignKey(mph=>mph.MedicamentId)
                .WillCascadeOnDelete(true);

            this.ToTable("MedicamentPriceHistory");
        }
    }
}
