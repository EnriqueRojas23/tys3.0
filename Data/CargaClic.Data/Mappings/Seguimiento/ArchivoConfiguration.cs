

using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class ArchivoConfiguration: IEntityTypeConfiguration<Archivo>
    {
        public void Configure(EntityTypeBuilder<Archivo> builder)
        {
            builder.ToTable("Archivo","Liquidacion");
            builder.HasKey(x=>x.idarchivo);
            builder.Property(x=>x.nombrearchivo).HasMaxLength(400).IsRequired();
            builder.Property(x=>x.rutaacceso).HasMaxLength(400).IsRequired();
        }
    }
}