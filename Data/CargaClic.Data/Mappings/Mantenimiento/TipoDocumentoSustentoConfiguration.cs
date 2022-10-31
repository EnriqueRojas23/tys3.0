using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class TipoDocumentoSustentoConfiguration : IEntityTypeConfiguration<TipoDocumentoSustento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumentoSustento> builder)
        {
            builder.ToTable("TipoDocumentoSustento","Seguimiento");
            builder.HasKey(x=>x.id);

        }
    }
}