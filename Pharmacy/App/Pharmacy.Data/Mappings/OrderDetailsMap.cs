using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Pharmacy.Core;

namespace Pharmacy.Data.Mappings
{
    public class OrderDetailsMap :EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsMap()
        {
            this.HasKey(od => new {od.OrderId, od.MedicamentId});

            this.Property(od => od.OrderId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(od => od.MedicamentId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(od => od.Quantity)
                .IsRequired();
            this.Property(od => od.Price)
                .IsRequired();

            this.HasRequired<Order>(od=>od.Order)
                .WithMany(o=>o.OrderDetailses)
                .HasForeignKey(od=>od.OrderId)
                .WillCascadeOnDelete(true);

            this.HasRequired<Medicament>(od => od.Medicament)
                .WithMany(m => m.OrderDetailses)
                .HasForeignKey(od => od.MedicamentId)
                .WillCascadeOnDelete(true);

            this.ToTable("OrderDetails");
        }
    }
}
