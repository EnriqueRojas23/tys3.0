using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class CuadrillaConfiguration: IEntityTypeConfiguration<Cuadrilla>
    {
    
        public void Configure(EntityTypeBuilder<Cuadrilla> builder)
        {
            builder.ToTable("Cuadrilla","Seguimiento");
            builder.HasKey(x=>x.id);
            
        }
    }
}