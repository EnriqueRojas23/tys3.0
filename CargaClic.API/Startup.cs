
using System.Net;
using System.Text;
using AutoMapper;
using CargaClic.Data;
using CargaClic.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using CargaClic.API.Data;
using CargaClic.Handlers.Seguridad;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguridad;
using Common;
using CargaClic.Domain.Mantenimiento;
using CargaClic.Handlers.Mantenimiento;
using CargaClic.ReadRepository.Interface.Mantenimiento;
using CargaClic.ReadRepository.Interface.Despacho;
using CargaClic.ReadRepository.Repository.Despacho;
using CargaClic.Domain.Despacho;
using CargaClic.Repository.Interface;
using CargaClic.Repository;
using CargaClic.Domain.Seguimiento;
using Common.QueryHandlers;
using CargaClic.Data.Contracts.Parameters.Seguridad;
using CargaClic.ReadRepository.Repository.Inventario;
using CargaClic.ReadRepository.Interface;

namespace CargaClic.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddDbContext<DataContextSecurity>(x=>x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
             services.AddDbContext<DataContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


             services.AddSingleton(_ => Configuration);
             services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
             services.AddCors();
             services.AddAutoMapper();
             services.AddTransient<Seed>();
             //services.AddTransient<Seed>();

             services.AddScoped<IRepository<User>,Repository<User>>();
             services.AddScoped<IRepository<Rol>,Repository<Rol>>();
             services.AddScoped<IRepository<RolPagina>,Repository<RolPagina>>();
             services.AddScoped<IRepository<Pagina>,Repository<Pagina>>();
             services.AddScoped<IRepository<RolUser>,Repository<RolUser>>();
             services.AddScoped<IAuthRepository,AuthRepository>();

             services.AddScoped<IRepository<Archivo>,Repository<Archivo>>();
             services.AddScoped<IRepository<Estado>,Repository<Estado>>();
             services.AddScoped<IRepository<Vehiculo>,Repository<Vehiculo>>();
             services.AddScoped<IRepository<Chofer>,Repository<Chofer>>();
             services.AddScoped<IRepository<Proveedor>,Repository<Proveedor>>();
             services.AddScoped<IRepository<OrdenTrabajo>,Repository<OrdenTrabajo>>();
             services.AddScoped<IRepository<Manifiesto>,Repository<Manifiesto>>();
             
             services.AddScoped<IRepository<Distrito>,Repository<Distrito>>();
             services.AddScoped<IRepository<Direccion>,Repository<Direccion>>();
             services.AddScoped<IRepository<TarifaProveedor>,Repository<TarifaProveedor>>();

              


            services.AddScoped<IRepository<Sustento>,Repository<Sustento>>();
            services.AddScoped<IRepository<SustentoDetalle>,Repository<SustentoDetalle>>();
            services.AddScoped<IRepository<TipoSustento>,Repository<TipoSustento>>();
            services.AddScoped<IRepository<TipoDocumentoSustento>,Repository<TipoDocumentoSustento>>();



             services.AddScoped<IRepository<GuiaRemisionBlanco>,Repository<GuiaRemisionBlanco>>();
             
             services.AddScoped<ISeguimientoRepository,SeguimientoRepository>();
             services.AddScoped<ISeguimientoReadRepository,SeguimientoReadRepository>();
             
             services.AddScoped<IMantenimientoReadRepository,MantenimientoReadRepository>();
             services.AddScoped<IRepository<CargaMasivaDetalle>,Repository<CargaMasivaDetalle>>();

             
       
         
     
    
            services.AddScoped<IRepository<ValorTabla>, Repository<ValorTabla>>();
            services.AddScoped<IRepository<Cuadrilla>, Repository<Cuadrilla>>();
  
            
            services.AddScoped<IDespachoReadRepository,DespachoReadRepository>();
            
            services.AddScoped<IQueryHandler<ListarMenusxRolParameter>,ListarMenusxRolQuery>();

                 

             
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(
                                   Encoding.ASCII.GetBytes(Configuration
                                .GetSection("AppSettings:Token").Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false                            
                            };
                        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                  app.UseExceptionHandler(builder=> { 
                    builder.Run(async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message); 
                            await context.Response.WriteAsync(error.Error.Message); 
                        }
                    });
                });
            }
            else
            {
                app.UseExceptionHandler(builder=> { 
                    builder.Run(async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message); 
                            await context.Response.WriteAsync(error.Error.Message); 
                        }
                    });
                });
               // app.UseHsts();
            }
            // app.UseHttpsRedirection();
            
            //seeder.SeedEstados();
           // seeder.SeedUsers();
            //seeder.SeedPaginas();
            //seeder.SeedRoles();
           // seeder.SeedRolPaginas();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}   
