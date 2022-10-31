using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class TipoSustentoConfiguration : IEntityTypeConfiguration<TipoSustento>
    {
        public void Configure(EntityTypeBuilder<TipoSustento> builder)
        {
            builder.ToTable("TipoSustento","Seguimiento");
            builder.HasKey(x=>x.id);

        }
    }
}