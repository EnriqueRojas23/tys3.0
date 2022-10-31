
using CargaClic.Domain.Mantenimiento;
using CargaClic.Domain.Seguridad;
using CargaClic.Data.Mappings.Seguridad;
using Microsoft.EntityFrameworkCore;
using CargaClic.Data.Mappings.Prerecibo;
using CargaClic.Domain.Despacho;
using CargaClic.Domain.Seguimiento;
using CargaClic.Data.Mappings.Seguimiento;

namespace CargaClic.Data
{
    public class DataContextSecurity : DbContext // Usar, modificar o ampliar métodos de esta clase
    {
        public DataContextSecurity(DbContextOptions<DataContext> options) : base(options) {}  
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolPagina> RolPaginas {get;set;}
        public DbSet<Archivo> Archivo {get;set;}
        public DbSet<Manifiesto> Manifiesto {get;set;}
        public DbSet<Carga> Carga {get;set;}
        public DbSet<ValorTabla> ValorTabla {get;set;}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PaginaConfiguration());
            builder.ApplyConfiguration(new RolConfiguration ());
            builder.ApplyConfiguration(new RolPaginaConfiguration());
            builder.ApplyConfiguration(new ArchivoConfiguration());
            builder.ApplyConfiguration(new ManifiestoConfiguration());
            builder.ApplyConfiguration(new CargaConfiguration());

            base.OnModelCreating(builder);

            builder.Entity<RolPagina>()
                .Property(x=>x.permisos).HasMaxLength(3).IsRequired();
            
            builder.Entity<RolPagina>()
                .ToTable("RolesPaginas","Seguridad")
                .HasKey(rp => new { rp.IdRol, rp.IdPagina });
                

            builder.Entity<RolPagina>()
                .HasOne(rp => rp.Pagina)
                .WithMany(p => p.RolPaginas)
                .HasForeignKey(p => p.IdPagina);
            builder.Entity<RolPagina>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPaginas)
                .HasForeignKey(r => r.IdRol);



            builder.Entity<RolUser>()
                .ToTable("RolesUsers","Seguridad")
                .HasKey(rp => new { rp.RolId, rp.UserId });
            builder.Entity<RolUser>()
                .HasOne(rp => rp.Rol)
                .WithMany(p => p.RolUser)
                .HasForeignKey(p => p.RolId);

        }
    }
}