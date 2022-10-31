

using CargaClic.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi.Data.Domain;

namespace CargaClic.Data.Mappings.Seguimiento
{
    public class EtapaConfiguration: IEntityTypeConfiguration<Etapa>
    {
        public void Configure(EntityTypeBuilder<Etapa> builder)
        {
            builder.ToTable("Etapa","Monitoreo");
            builder.HasKey(x=>x.idetapa);

        }
    }
}