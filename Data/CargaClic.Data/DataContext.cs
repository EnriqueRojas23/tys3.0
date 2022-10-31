
using CargaClic.Domain.Seguridad;
using CargaClic.Data.Mappings.Seguridad;
using Microsoft.EntityFrameworkCore;
using CargaClic.Data.Mappings.Prerecibo;
using CargaClic.Domain.Despacho;
using CargaClic.Domain.Seguimiento;
using CargaClic.Data.Mappings.Seguimiento;
using CargaClic.Data.Mappings.Mantenimiento;
using CargaClic.Domain.Mantenimiento;
using Api.Data.Mappings.Mantenimiento;
using Webapi.Data.Domain;

namespace CargaClic.Data
{
    public class DataContext : DbContext // Usar, modificar o ampliar métodos de esta clase
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}  
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolPagina> RolPaginas {get;set;}
        
        
         public DbSet<Vehiculo> Vehiculos {get;set;}
         public DbSet<Proveedor> Proveedores {get;set;}
         public DbSet<Chofer> Choferes {get;set;}

         public DbSet<Etapa> Etapas { get; set; }
        

        public DbSet<Archivo> Archivo {get;set;}
        public DbSet<Manifiesto> Manifiesto {get;set;}
        public DbSet<Carga> Carga {get;set;}
        public DbSet<CargaMasiva> CargaMasiva {get;set;}

         public DbSet<ValorTabla> ValorTablas {get;set;}
         public DbSet<GuiaRemisionCliente> guiaRemisionClientes {get;set;}

        public DbSet<CargaMasivaDetalle> CargaMasivaDetalle {get;set;}

        public DbSet<OrdenTrabajo> OrdenTrabajo {get;set;}
        public DbSet<OrdenRecojo> OrdenRecojo {get;set;}
        public DbSet<Cuadrilla> Cuadrilla {get;set;}
        public DbSet<Distrito> Distritos {get;set;}
        public DbSet<Direccion> Direcciones {get;set;}

        public DbSet<TipoSustento> tipoSustentos {get;set;}
        public DbSet<TipoDocumentoSustento> tipoDocumentoSustentos {get;set;}

        public DbSet<Sustento> sustentos {get;set;}
        public DbSet<SustentoDetalle> sustentoDetalles {get;set;}

            
        public DbSet<GuiaRemisionBlanco> guiaRemisionBlancos {get;set;}

    
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new TipoSustentoConfiguration());
            builder.ApplyConfiguration(new TipoDocumentoSustentoConfiguration());

             builder.ApplyConfiguration(new SustentoConfiguration());
            builder.ApplyConfiguration(new SustentoDetalleConfiguration());
            builder.ApplyConfiguration(new ValorTablaConfiguration());
            builder.ApplyConfiguration(new EtapaConfiguration());


            builder.ApplyConfiguration(new PaginaConfiguration());
            builder.ApplyConfiguration(new RolConfiguration ());
            builder.ApplyConfiguration(new RolPaginaConfiguration());
            builder.ApplyConfiguration(new DistritoConfiguration());

             builder.ApplyConfiguration(new GuiaRemisionClienteConfiguration());
            
           
            builder.ApplyConfiguration(new CargaMasivaConfiguration());
            builder.ApplyConfiguration(new CargaMasivaDetalleConfiguration());

            builder.ApplyConfiguration(new ArchivoConfiguration());
            builder.ApplyConfiguration(new VehiculoConfiguration());
            builder.ApplyConfiguration(new ChoferConfiguration());
            builder.ApplyConfiguration(new ProveedorConfiguration());
            builder.ApplyConfiguration(new TarifaProveedorConfiguration());


            builder.ApplyConfiguration(new OrdenTrabajoConfiguration());
            builder.ApplyConfiguration(new OrdenRecojoConfiguration());
            builder.ApplyConfiguration(new CuadrillaConfiguration());

            builder.ApplyConfiguration(new ManifiestoConfiguration());
            builder.ApplyConfiguration(new DireccionConfiguration());


            builder.ApplyConfiguration(new CargaConfiguration());
            builder.ApplyConfiguration(new GuiaRemisionBlancoConfiguration());

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