using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class    OrdenRecojoConfiguration: IEntityTypeConfiguration<OrdenRecojo>
    {
    
        public void Configure(EntityTypeBuilder<OrdenRecojo> builder)
        {
            builder.ToTable("OrdenRecojo","Seguimiento");
            builder.HasKey(x=>x.idordenrecojo);
            
        }
    }
}