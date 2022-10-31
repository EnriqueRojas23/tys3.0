
using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Prerecibo
{
    public class ManifiestoConfiguration: IEntityTypeConfiguration<Manifiesto>
    {
        public void Configure(EntityTypeBuilder<Manifiesto> builder)
        {
            builder.ToTable("Manifiesto","Seguimiento");
            builder.HasKey(x=>x.idmanifiesto);
            builder.Property(x=>x.numhojaruta).HasMaxLength(10);
        }
    }
}