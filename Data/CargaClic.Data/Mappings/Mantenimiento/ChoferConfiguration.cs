using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class ChoferConfiguration : IEntityTypeConfiguration<Chofer>
    {
        public void Configure(EntityTypeBuilder<Chofer> builder)
        {
            builder.ToTable("Chofer","Seguimiento");
            builder.HasKey(x=>x.idchofer);
            builder.Property(x=>x.nombrechofer).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.apellidochofer).HasMaxLength(11).IsRequired();
            builder.Property(x=>x.dni).HasMaxLength(11).IsRequired();
            
            

        }
    }
}