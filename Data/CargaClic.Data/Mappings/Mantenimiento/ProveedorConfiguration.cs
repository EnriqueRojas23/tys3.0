using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedor","Pago");
            builder.HasKey(x=>x.idproveedor);
            builder.Property(x=>x.RazonSocial).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.Ruc).HasMaxLength(11).IsRequired();
        }
    }
}