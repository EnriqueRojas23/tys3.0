using CargaClic.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Mantenimiento
{
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.ToTable("Distrito","Seguimiento");
            builder.HasKey(x=>x.iddistrito);
            builder.Property(x=>x.distrito).HasMaxLength(50).IsRequired();

        }
    }
}