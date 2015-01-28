using System.Data.Entity.ModelConfiguration;
using Pharmacy.Core;

namespace Pharmacy.Data.Mappings
{
    public class MedicamentMap : EntityTypeConfiguration<Medicament>
    {
        public MedicamentMap()
        {
            this.HasKey(m => m.Id);

            this.Property(m => m.Name)
                .IsRequired();
            this.Property(m => m.SerialNumber)
                .IsRequired();
            this.Property(m => m.Description)
                .IsRequired();
            this.Property(m => m.Price)
                .IsOptional();

            this.ToTable("Medicament");
        }
    }
}
