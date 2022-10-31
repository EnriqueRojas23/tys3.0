using System.Threading.Tasks;
using AutoMapper;
using CargaClic.Data.Interface;
using CargaClic.Domain.Mantenimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using CargaClic.ReadRepository.Interface.Mantenimiento;
using System.Collections.Generic;
using CargaClic.API.Dtos.Matenimiento;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CargaClic.API.Controllers.Mantenimiento
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        
        private readonly IMantenimientoReadRepository _repoMantenimientoRead;
        private readonly IRepository<Estado> _repo;
        private readonly IRepository<Vehiculo> _repovehiculo;

        private readonly IRepository<TipoSustento> _tipoSustento;

        private readonly IRepository<Sustento> _Sustento;

        private readonly IRepository<SustentoDetalle> _SustentoDetalle;
        private readonly IRepository<Chofer> _repoChofer;
        private readonly IRepository<Proveedor> _repoproveedor;

        private readonly IRepository<TarifaProveedor> _repoTarifaProveedor;
        private readonly IRepository<ValorTabla> _repoValorTabla;
        private readonly IMapper _mapper;


        public GeneralController(
         IMantenimientoReadRepository repoMantenimientoRead
        , IRepository<Estado> repo
        , IRepository<Vehiculo> repovehiculo
        , IRepository<Chofer> repoChofer
        , IRepository<Proveedor> repoproveedor
        , IRepository<ValorTabla> repoValorTabla
        , IMapper mapper
        , IRepository<TipoSustento> tipoSustento, IRepository<Sustento> sustento = null, IRepository<SustentoDetalle> sustentoDetalle = null, IRepository<TarifaProveedor> repoTarifaProveedor = null)
        {

            _repoMantenimientoRead = repoMantenimientoRead;
            _repo = repo;
            _repovehiculo = repovehiculo;
            _repoChofer = repoChofer;
            _repoproveedor = repoproveedor;
            _repoValorTabla = repoValorTabla;
            _mapper = mapper;
            _tipoSustento = tipoSustento;
            _Sustento = sustento;
            _SustentoDetalle = sustentoDetalle;
            _repoTarifaProveedor = repoTarifaProveedor;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int TablaId)
        {
           var result = await _repo.GetAll(x=>x.TablaId == TablaId);
           
           return Ok(result);
        }
        [HttpGet("GetAllValorTabla")]
        public async Task<IActionResult> GetAllValorTabla(int TablaId)
        {
           var result = await _repoValorTabla.GetAll(x=>x.idmaestrotabla == TablaId);
           
           return Ok(result);
        }

        #region _repoVehiculo
        [HttpGet("GetVehiculos")]
        public async Task<IActionResult> GetVehiculos(string placa)
        {
            
            if(placa == null)  return Ok(await _repovehiculo.GetAll(x=>x.activo == true));

            var result = await _repovehiculo.Get(x=>x.placa == placa);
            return Ok(result);
        }
        [HttpGet("GetVehiculo")]
        public async Task<IActionResult> GetVehiculo(int id)
        {
           var placa = await _repovehiculo.Get(x=>x.idvehiculo == id);
           return Ok(placa);
        }
        
      
#endregion


#region _repoProveedor

        [HttpGet("GetProveedores")]
        public async Task<IActionResult> GetProveedores(string criterio)
        {
            var proveedores = await  _repoproveedor.GetAll();

          
            if(criterio ==  "" || criterio == null)
            {
                return Ok(proveedores);
            }
            
            var filtro = (from t in proveedores
                         where EF.Functions.Like(t.RazonSocial, "%" + criterio + "%")
                         select t).ToList();

            return Ok(filtro);
         
        }
         [HttpGet("GetProveedor")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var proveedor = await _repoproveedor.Get(x=>x.idproveedor == id);
            return Ok(proveedor);
         
        }
        [HttpGet("GetAllTarifarioProveedor")]
        public async Task<IActionResult> GetAllTarifarioProveedor(int idproveedor)
        {
           var result = await  _repoMantenimientoRead.GetAllTarifarioProveedor(idproveedor);
           return Ok(result);
        }
        [HttpPost("InsertTarifaProveedor")]
        public async Task<IActionResult> InsertTarifaProveedor(TarifaProveedorForRegisterDto tarifa)
        {
            var obj = new TarifaProveedor();
            obj.iddestinodepartamento = tarifa.iddestinodepartamento;
            obj.iddestinodistrito = tarifa.iddestinodistrito;
            obj.iddestinoprovincia = tarifa.iddestinoprovincia;
            obj.idproveedor = tarifa.idproveedor;
            obj.precio = tarifa.precio;
            obj.idtipounidad = tarifa.idtipounidad;

            obj.adicional = tarifa.adicional;
            obj.desde = tarifa.desde;
            obj.hasta = tarifa.hasta;
            obj.minimo = tarifa.minimo;
            obj.primerkilo = tarifa.primerkilo;

            obj.idorigendistrito = 7;

            var resp = await _repoTarifaProveedor.AddAsync(obj);
            await _repoTarifaProveedor.SaveAll();
   
            return Ok(resp);
         
        }
        [HttpPost("UpdateTarifaProveedor")]
        public async Task<IActionResult> UpdateTarifaProveedor(TarifaProveedorForRegisterDto tarifa)
        {

            var obj = await _repoTarifaProveedor.Get(x=>x.id ==  tarifa.id);

           
            obj.iddestinodepartamento = tarifa.iddestinodepartamento;
            obj.iddestinodistrito = tarifa.iddestinodistrito;
            obj.iddestinoprovincia = tarifa.iddestinoprovincia;
       //     obj.idproveedor = tarifa.idproveedor;
            obj.precio = tarifa.precio;
            obj.idtipounidad = tarifa.idtipounidad;

            obj.idorigendistrito = 7;

            
            await _repoTarifaProveedor.SaveAll();
   
            return Ok(obj);
         
        }
        [HttpPost("DeleteTarifaProveedor")]
        public async Task<IActionResult> DeleteTarifaProveedor(int id)
        {
        
            var tarifa = await _repoTarifaProveedor.Get(x=>x.id == id);
             _repoTarifaProveedor.Delete(tarifa);
            await _repoTarifaProveedor.SaveAll();
   
            return Ok(true);
         
        }
        [HttpGet("GetTarifaProveedor")]
        public async Task<IActionResult> GetTarifaProveedor(int id)
        {
            var tarifa = await _repoTarifaProveedor.Get(x=>x.id == id);

            

            return Ok(tarifa);
         
        }

      

#endregion

#region _repoChofer

        [HttpGet("GetChofer")]
        public async Task<IActionResult> GetChofer(string criterio)
        {
            
            if(criterio == null)  return Ok(await _repoChofer.GetAll());

            var result = await _repoChofer.GetAll(x=>x.dni.Contains(criterio)
            || x.apellidochofer.Contains(criterio) );
            
       
            return Ok(result);
        }
        [HttpGet("GetChoferxId")]
        public async Task<IActionResult> GetChofer(int id)
        {
            var result = await _repoChofer.Get(x=>x.idchofer == id);
            return Ok(result);
        }
        [HttpPost("RegisterChofer")]
        public async Task<IActionResult> RegisterChofer(Chofer chofer)
        {

            try
            {
                var createdChofer = await _repoChofer.AddAsync(chofer);
                return Ok(createdChofer);    
            }
            catch (System.Exception)
            {
                throw new ArgumentException("El conductor ya existe");
            }
            
        }
       

        [HttpGet("GetChoferes")]
        public async Task<IActionResult> GetChoferes()
        {
            var result = await _repoChofer.GetAll();
            return Ok(result);
        }

#endregion          

        [HttpGet("GetAllDepartamentos")]
        public async Task<IActionResult> GetAllDepartamentos()
        {
            var resp = await  _repoMantenimientoRead.GetAllDepartamentos();
            return Ok(resp);
        }
        [HttpGet("GetAllProvincias")]
        public async Task<IActionResult> GetAllProvincias(int DepartamentoId)
        {
            var resp = await  _repoMantenimientoRead.GetAllProvincias(DepartamentoId);
            return Ok(resp);
        }
        [HttpGet("GetAllDistritos")]
        public async Task<IActionResult> GetAllDistritos(int ProvinciaId)
        {
            var resp = await  _repoMantenimientoRead.GetAllDistritos(ProvinciaId);
            return Ok(resp);
        }

        
    


      

    }
}