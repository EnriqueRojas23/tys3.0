

using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings.Mantenimiento
{
    public class ValorTablaConfiguration : IEntityTypeConfiguration<ValorTabla>
    {
        public void Configure(EntityTypeBuilder<ValorTabla> builder)
        {
            builder.ToTable("ValorTabla","Seguimiento");
            builder.HasKey(x=>x.idvalortabla);
            builder.Property(x=>x.valor).HasMaxLength(100).IsRequired();
            
        }
    }
}