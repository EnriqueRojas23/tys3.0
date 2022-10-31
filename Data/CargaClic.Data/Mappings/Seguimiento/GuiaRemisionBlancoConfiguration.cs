using CargaClic.Domain.Despacho;
using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class GuiaRemisionBlancoConfiguration: IEntityTypeConfiguration<GuiaRemisionBlanco>
    {
        public void Configure(EntityTypeBuilder<GuiaRemisionBlanco> builder)
        {
            builder.ToTable("GuiaRemisionBlanco","Seguimiento");
            builder.HasKey(x=>x.id);
        }
    }
}