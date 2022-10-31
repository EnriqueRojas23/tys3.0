using CargaClic.Domain.Despacho;
using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class GuiaRemisionClienteConfiguration: IEntityTypeConfiguration<GuiaRemisionCliente>
    {
        public void Configure(EntityTypeBuilder<GuiaRemisionCliente> builder)
        {
            builder.ToTable("GuiaRemisionCliente","Seguimiento");
            builder.HasKey(x=>x.idguiaremisioncliente);
        }
    }
}