
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

using CargaClic.Data.Contracts.Parameters.Seguridad;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguridad;
using Common;
using CargaClic.Contracts.Parameters.Prerecibo;
using CargaClic.Handlers.Precibo;
using CargaClic.Domain.Mantenimiento;
using CargaClic.Domain.Prerecibo;
using CargaClic.Contracts.Parameters.Mantenimiento;
using CargaClic.Handlers.Mantenimiento;

using CargaClic.Contracts.Parameters.Inventario;
using CargaClic.Handlers.Inventario;
using CargaClic.Domain.Inventario;

using CargaClic.ReadRepository.Interface.Mantenimiento;
using CargaClic.ReadRepository.Interface.Inventario;
using CargaClic.ReadRepository.Repository.Inventario;
using CargaClic.ReadRepository.Interface.Despacho;
using CargaClic.ReadRepository.Repository.Despacho;
using CargaClic.Domain.Despacho;
using CargaClic.ReadRepository.Interface.Facturacion;
using CargaClic.Domain.Facturacion;
using CargaClic.Repository.Interface;
using CargaClic.Repository;

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
             services.AddScoped<IRepository<Estado>,Repository<Estado>>();

             
            services.AddScoped<ISeguimientoRepository,SeguimientoRepository>();
             services.AddScoped<IMantenimientoRepository,MantenimientoRepository>();
             services.AddScoped<IAuthRepository,AuthRepository>();
         
     
            services.AddScoped<IRepository<Proveedor>, Repository<Proveedor>>();
            services.AddScoped<IRepository<Vehiculo>, Repository<Vehiculo>>();
            services.AddScoped<IRepository<Huella>, Repository<Huella>>();
            services.AddScoped<IRepository<Producto>, Repository<Producto>>();
            services.AddScoped<IRepository<Chofer>, Repository<Chofer>>();
            services.AddScoped<IRepository<ValorTabla>, Repository<ValorTabla>>();
            services.AddScoped<IRepository<Preliquidacion>, Repository<Preliquidacion>>();
            services.AddScoped<IRepository<Almacen>, Repository<Almacen>>();

            
           
            services.AddScoped<IInventarioReadRepository,InventarioReadRepository>();
            services.AddScoped<IRepository<OrdenSalidaDetalle>,Repository<OrdenSalidaDetalle>>();
            services.AddScoped<IDespachoReadRepository,DespachoReadRepository>();
            services.AddScoped<IFacturacionReadRepository,FacturacionReadRepository>();
            services.AddScoped<IRepository<Documento>,Repository<Documento>>();
            services.AddScoped<IRepository<Propietario>,Repository<Propietario>>();
            services.AddScoped<IRepository<Area>,Repository<Area>>();
            services.AddScoped<IRepository<InventarioGeneral>,Repository<InventarioGeneral>>();
                 

             
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
