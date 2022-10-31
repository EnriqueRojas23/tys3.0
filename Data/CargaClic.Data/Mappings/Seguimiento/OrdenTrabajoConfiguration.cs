using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class OrdenTrabajoConfiguration: IEntityTypeConfiguration<OrdenTrabajo>
    {
    
        public void Configure(EntityTypeBuilder<OrdenTrabajo> builder)
        {
            builder.ToTable("OrdenTrabajo","Seguimiento");
            builder.HasKey(x=>x.idordentrabajo);
            builder.Property(x=>x.idcliente).IsRequired();
        }
    }
}