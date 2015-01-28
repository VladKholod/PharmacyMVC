using System.Data.Entity.ModelConfiguration;

namespace Pharmacy.Data.Mappings
{
    public class PharmacyMap : EntityTypeConfiguration<Core.Pharmacy>
    {
        public PharmacyMap()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.Address)
                .IsRequired();
            this.Property(p => p.Number)
                .IsRequired();
            this.Property(p => p.Phone)
                .IsRequired();
            this.Property(p => p.OpenDate)
                .IsRequired();

            this.ToTable("Pharmacy");
        }
    }
}
