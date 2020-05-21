
using System.Threading.Tasks;
using AutoMapper;
using CargaClic.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CargaClic.API.Controllers.Despacho
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {

   
        private readonly ISeguimientoRepository _repoSeguimiento;
        private readonly IMapper _mapper;

        public SeguimientoController(
         ISeguimientoRepository repo_Seguimiento,
         IMapper mapper) {
            _repoSeguimiento = repo_Seguimiento;
            _mapper = mapper;
        }
    
     
 
      [HttpGet("GetAllOrder")]
      public async Task<IActionResult> GetAllOrder(int? idcliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int idusuario)
      {           
          var resp  = await _repoSeguimiento.GetAllOrdenTransporte(idcliente , numcp
          , fecinicio
          , fecfin , grr, docreferencia, idestado, iddestino, idusuario );
          return Ok (resp);
      }
      
      [HttpGet("GetAllClients")]
      public async Task<IActionResult> GetAllClients(string idscliente)
      { 
          var resp  = await _repoSeguimiento.GetAllClientes(idscliente);
          return Ok (resp);
      }
      [HttpGet("GetListUbigeo")]
      public async Task<IActionResult> GetListUbigeo(string criterio)
      { 
          var resp  = await _repoSeguimiento.GetListarUbigeo(criterio);
          return Ok (resp);
      }
    

    }
}