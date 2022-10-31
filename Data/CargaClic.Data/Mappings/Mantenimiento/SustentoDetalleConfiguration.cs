using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class SustentoDetalleConfiguration : IEntityTypeConfiguration<SustentoDetalle>
    {
        public void Configure(EntityTypeBuilder<SustentoDetalle> builder)
        {
            builder.ToTable("SustentoDetalle","Seguimiento");
            builder.HasKey(x=>x.id);

        }
    }
}