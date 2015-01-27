using System.Data.Entity.ModelConfiguration;
using Pharmacy.Core;

namespace Pharmacy.Data.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.HasKey(o => o.Id);

            this.Property(o => o.Date)
                .IsRequired();
            this.Property(o => o.Type)
                .IsRequired();
            this.Property(o => o.PharmacyId)
                .IsRequired();

            this.HasRequired<Core.Pharmacy>(o=>o.Pharmacy)
                .WithMany(p=>p.Orders)
                .HasForeignKey(o=>o.PharmacyId)
                .WillCascadeOnDelete(true);

            this.ToTable("Order");
        }
    }
}
