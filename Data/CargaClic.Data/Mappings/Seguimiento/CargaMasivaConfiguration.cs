
using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class CargaMasivaConfiguration: IEntityTypeConfiguration<CargaMasiva>
    {
        public void Configure(EntityTypeBuilder<CargaMasiva> builder)
        {
            builder.ToTable("CargaMasiva","Seguimiento");
            builder.HasKey(x=>x.id);
            builder.Property(x=>x.fecharegistro).IsRequired();
            builder.Property(x=>x.usuarioid).IsRequired();
            
        }
    }
}