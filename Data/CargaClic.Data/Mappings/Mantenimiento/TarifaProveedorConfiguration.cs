using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class TarifaProveedorConfiguration : IEntityTypeConfiguration<TarifaProveedor>
    {
        public void Configure(EntityTypeBuilder<TarifaProveedor> builder)
        {
            builder.ToTable("TarifaProveedor","seguimiento");
            builder.HasKey(x=>x.id);
            builder.Property(x=>x.idproveedor).IsRequired();
            builder.Property(x=>x.precio).IsRequired();
            builder.Property(x=>x.idtipounidad).IsRequired();
        }
    }
}