
using System.Threading.Tasks;
using AutoMapper;
using CargaClic.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguimiento;
using System.Linq;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using CargaClic.Repository;
using CargaClic.ReadRepository.Interface;
using System.Collections.Generic;
using System;
using CargaClic.API.Dtos.Recepcion;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using CargaClic.Repository.Contracts.Seguimiento;
using CargaClic.Domain.Mantenimiento;
using Api.ReadRepository.Contracts.Mantenimiento.Results;
using Api.ReadRepository.Mantenimiento.Parameters;
using System.Data;
using CargaClic.API.Dtos.Seguimiento;
using CargaClic.API.Dtos.Matenimiento;
using Webapi.Data.Commands.QueryContracts;
using Webapi.Common;
using Webapi.Data.Domain;

namespace CargaClic.API.Controllers.Despacho
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly ISeguimientoRepository _repoSeguimiento;
        private readonly ISeguimientoReadRepository _repoReadSeguimiento;
        private readonly IMapper _mapper;
        private readonly IRepository<Archivo> _archivo;
        private readonly IRepository<OrdenTrabajo> _repoordentrabajo;
        private readonly IRepository<Cuadrilla> _cuadrilla;
        private readonly IRepository<Manifiesto> _repomanifiesto;
        private readonly IRepository<GuiaRemisionBlanco> _guiaremisionblanco;
        private readonly IRepository<CargaMasivaDetalle> _repo_CargaMasiva;

        private readonly IRepository<Distrito> _repoDistrito;

        private readonly IRepository<Direccion> _repoDireccion;

        private readonly IRepository<Vehiculo> _repoVehiculo;

        private readonly IRepository<Sustento> _Sustento;

        private readonly IRepository<SustentoDetalle> _SustentoDetalle;



        public SeguimientoController(
         IConfiguration config,
         ISeguimientoRepository repo_Seguimiento,
         ISeguimientoReadRepository repo_ReadSeguimiento,
         IRepository<Archivo> archivo,
         IRepository<OrdenTrabajo> repoordentrabajo,
         IMapper mapper, IRepository<GuiaRemisionBlanco> guiaremisionblanco,
         IRepository<Manifiesto> repomanifiesto,
         IRepository<Cuadrilla> cuadrilla,
         IRepository<CargaMasivaDetalle> repo_CargaMasiva,
         IRepository<Distrito> repo_Distrito,

         IRepository<Distrito> repoDistrito,
         IRepository<Direccion> repoDireccion, IRepository<Sustento> sustento, IRepository<SustentoDetalle> sustentoDetalle, IRepository<Vehiculo> repoVehiculo)
        {
            _repoSeguimiento = repo_Seguimiento;
            _mapper = mapper;
            _config = config;
            _archivo = archivo;
            _repoReadSeguimiento = repo_ReadSeguimiento;
            _repoordentrabajo = repoordentrabajo;
            _guiaremisionblanco = guiaremisionblanco;
            _repomanifiesto = repomanifiesto;
            _cuadrilla = cuadrilla;
            _repo_CargaMasiva = repo_CargaMasiva;
            _repoDistrito = repoDistrito;
            _repoDireccion = repoDireccion;
            _Sustento = sustento;
            _SustentoDetalle = sustentoDetalle;
            _repoVehiculo = repoVehiculo;
        }
        [HttpPost("UploadPhoto")]
        [DisableRequestSizeLimit]
        public IActionResult UploadPhoto(Int64 idOrden)
        {
            try
            {

                var ruta =  _config.GetSection("AppSettings:Uploads").Value;

                var file = Request.Form.Files[0];
                //var idOrden = Request.Form["idOrden"];
                var archivo = new Archivo() ;   



                string folderName = idOrden.ToString() ;
                string webRootPath = ruta ;
                string newPath = Path.Combine(webRootPath, folderName);


                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(Request.Form.Files[0].OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes(Request.Form.Files[0].ContentDisposition.Length);
                }

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName =  DateTimeOffset.Now.ToUnixTimeMilliseconds() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);

                    var checkextension = Path.GetExtension(fileName).ToLower();


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {

                        file.CopyTo(stream);

                        archivo.extension = checkextension;
                        archivo.nombrearchivo =   fileName  ;
                        archivo.rutaacceso = newPath;
                        archivo.idordentrabajo = idOrden;

                         _archivo.AddAsync(archivo);
                         _archivo.SaveAll();

                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                 return Ok();
            }
        }
        [HttpPost ("procesarCargaMasiva")]
        public async Task<IActionResult> procesarCargaMasiva (CargaMasivaDto obj)
        {

            var detalles_cargados = await  _repo_CargaMasiva.GetAll(x=>x.cargaid == obj.cargaid);
            List<OrdenTrabajoForRegister> objAux = new List<OrdenTrabajoForRegister>();

            //agrupar por cliente

            var detalles_agrupados =   detalles_cargados.GroupBy(x => x.clientnum);

            foreach (var agrupado in detalles_agrupados)
            {
                List<string> guias = new List<string>();
                foreach (var item2 in agrupado)
                {
                     guias.Add(item2.numguia);
                   
                }
                foreach (var item2 in agrupado)
                {
                   objAux.AddRange(await ObtenerEntidades(item2, guias ,  obj.idcliente, agrupado.Count()));
                   break;
                }
            }


            var lista = detalles_cargados.ToList();
            await _repoSeguimiento.RegistrarOrdenTransporte(objAux);

            return Ok(detalles_cargados);
        }

        [HttpPost("UploadFile")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(int usrid, int idcliente)
        {
          // var seguimiento = new Seguimiento();
            try
            {
                // Grabar Excel en disco
                string fullPath = await SaveFile(0);
                // Leer datos de excel
                var celdas = GetExcel(fullPath);
                // Generar entidades
                var entidades = ObtenerEntidades_CargaMasiva(celdas, idcliente);
                // Grabar entidades en base de datos

                 var carga = new CargaMasivaForRegister();
                 carga.estadoid = 1;
                 carga.fecharegistro = DateTime.Now;

                 var resp =  await _repoSeguimiento.RegisterCargaMasiva(carga, entidades);

                  //Generar Ordenes de trabajo y Manifiestos
                  var detalles_cargados =  await _repo_CargaMasiva.GetAll(x=>x.cargaid == resp);

                  return Ok(detalles_cargados);



            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
                throw ex;

            }
            return Ok();
         }

        public async Task<List<OrdenTrabajoForRegister>> ObtenerEntidades(CargaMasivaDetalle detalles_cargados
        , List<string> guias, int idcliente, int bultos )
        {
               List<OrdenTrabajoForRegister> ordenes  = new List<OrdenTrabajoForRegister>();
               OrdenTrabajoForRegister orden = null;


            //    foreach (var command in detalles_cargados)
            //    {
                    orden = new OrdenTrabajoForRegister();
                    orden.guias = new List<string>();
                    orden.activo = true;

                    orden.idorigen = 44;

                    var tem = await _repoDistrito.GetAll(x => x.distrito ==  detalles_cargados.addr2);
                    if(tem.ToList().Count() < 1 )
                    {
                        throw new ArgumentException("No existe el distrito: " +  detalles_cargados.addr2);
                    }

                     var oDistrito =   tem.ToList()[0];
                     orden.iddestino = oDistrito.iddistrito;

                     orden.ordernum = detalles_cargados.ordernum;
                     orden.clientnum = detalles_cargados.clientnum;

                     orden.guiatransportista = detalles_cargados.numguia;
                     orden.idtarifa = 10702;
                     orden.dnipersonarecojo = detalles_cargados.clientnum;
                     orden.idcliente =  idcliente;

                 //    orden.guiatransportista = command.guianum;

                     if(orden.idcliente == 11200 )
                     {
                         orden.idformula = 10;
                     }
                     else {
                            orden.idformula = 8;
                     }


                     orden.idconceptocobro = 117;

                    if(orden.idcliente == 11200 )
                     {
                         orden.iddestinatario = 11200;
                     }
                     else {
                              orden.iddestinatario = 8161;

                     }

                     orden.idusuarioregistro = 2;

                     orden.identregara = 61;
                     orden.idestacionorigen = 2;

                     orden.idtipomercaderia = 118;
                     orden.idtipotransporte = 59;
                     orden.personarecojo = detalles_cargados.lastname;
                     orden.telefonorecojo = detalles_cargados.busphone;

                     orden.guiarecojo = "100-000001";
                     orden.guiatercero = "100-000001";
                     orden.idvehiculo = 84;

                     if(detalles_cargados.peso != string.Empty)
                     {
                         orden.peso = Convert.ToDecimal(detalles_cargados.peso);
                     }
                     if(detalles_cargados.pesovol != string.Empty)
                     {
                        orden.pesovol = Convert.ToDecimal(detalles_cargados.pesovol);
                     }

                     orden.volumen =0;
                     orden.bulto = bultos;
                     
                     orden.docgeneral = detalles_cargados.waybillnum;
                     orden.iddescripciongeneral = 845;
                     orden.total = 0;

                     var SubTotal = await CalcularCobro(orden.idorigen
                         , orden.iddestino,orden.idcliente,orden.idformula,orden.idtipotransporte ,orden.peso,
                         orden.pesovol, orden.bulto,orden.volumen, 0 );


                    var IGV = Math.Round(Convert.ToDouble(SubTotal) * 0.18, 2);
                    var Total = Math.Round(Convert.ToDouble(SubTotal) + IGV, 2);

                    orden.subtotal = Convert.ToDecimal(SubTotal);
                    orden.igv = Convert.ToDecimal(IGV);
                    orden.total = Convert.ToDecimal(Total);


                     orden.dni = "40730947";
                     orden.placa ="AYK-870";
                     orden.chofer = "CARLOS FLORES TOCTO";

                     orden.puntopartida = "AV. CORONEL NESTOR GAMBETTA N° 3235 - CALLAO";


                     orden.fecharecojo = DateTime.Now;

                     orden.idestacioncreacion = 2;
                     orden.direccion = detalles_cargados.addr1;

                     orden.idestado = 6;
                     orden.fecharegistro = DateTime.Now;
                     orden.idmanifiesto = null;
                     orden.idcamioncompleto = null;
                     orden.facturado = false;
                     orden.idestadocliente = 29;


                    foreach (var item in guias)
                    {
                          orden.guias.Add(item);
                    }
                   


                     // orden.subtotalfinal = command.subtotal;
                 ordenes.Add(orden);
               //}
               return ordenes;

        }

        public async Task<double> CalcularCobro(int idorigen, int iddestino, int idcliente,
int idformula, int idtipotransporte, decimal peso, decimal? pesovol, int bulto , decimal? volumen,int idtipomaterial)
        {
            GetAllTarifarioResult result = new GetAllTarifarioResult();


            var tot_result = await evaluarTarifasOrden_OrigenDistrito(idorigen, iddestino, idcliente, idformula, idtipotransporte, idtipomaterial);
            if (tot_result.Count == 0)
                tot_result = await evaluarTarifasOrden_OrigenProvincias(idorigen, iddestino, idcliente, idformula, idtipotransporte,idtipomaterial);
            if (tot_result.Count == 0)
                tot_result = await evaluarTarifasOrden_OrigenDepartamento(idorigen, iddestino, idcliente, idformula, idtipotransporte,idtipomaterial);

            foreach (var item in tot_result)
            {
                if (item.hasta == 0 && item.desde == 0)
                {
                    result = item;
                    break;
                }

                if (peso <= item.hasta && peso >= item.desde)
                {
                    result = item;
                }
            }

                 var Base = result.montobase;
                 var formula = "";


                if (idformula == 8 )
                {
                    formula = "Base + Precio * KG";
                }
                else if(idformula == 10)
                {
                       formula = "Base + Precio * Bulto";
                }
                else if(idformula == 3)
                {
                       formula = "Base + Precio * M3";
                }
                else if(idformula == 4)
                {
                       formula = "Precio";
                }

                formula = formula.Replace("Base", result.montobase.ToString());
                formula = formula.Replace("Precio", result.precio.ToString());

                if (pesovol > peso)
                    formula = formula.Replace("KG", pesovol.ToString());
                else
                    formula = formula.Replace("KG", peso.ToString());

                formula = formula.Replace("Bulto", bulto.ToString());
                formula = formula.Replace("M3", volumen.ToString());
                formula = formula.Replace("PesoVol", pesovol.ToString());

                var minimo = result.minimo;
                var base1 = result.montobase;
                var tarifa = result.precio;

                var SubTotal = Math.Round(Evaluate(formula), 2);


                if (SubTotal < Double.Parse(result.minimo.ToString()))
                {
                    SubTotal = Double.Parse(result.minimo.ToString());
                }

                var IGV = Math.Round(SubTotal * 0.18, 2);
                var Total = Math.Round(SubTotal + IGV, 2);




             return SubTotal;
        }
         private async  Task<List<GetAllTarifarioResult>> evaluarTarifasOrden_OrigenDepartamento(int idorigen
        , int iddestino, int idcliente, int idformula, int idtipotransporte,int idtipomaterial)
        {

            var ubigeo = await _repoReadSeguimiento.GetAllUbigeo("");


            var parametersaux = new ListarTarifaOrdenParameters
            {
                idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                iddistrito = iddestino,
                idcliente = idcliente,
                idformula = idformula,
                idtipotransporte = idtipotransporte,
                idtipomaterial = idtipomaterial

            };
            var resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            //Traer provincia del distrito

            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();
            if (resultado.ToList().Count == 0)
            {
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial

                };
               resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            }
            if (resultado.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial
                };
                resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

               // if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.ToList();
        }

        private async Task<List<GetAllTarifarioResult>> evaluarTarifasOrden_OrigenProvincias(int idorigen, int iddestino
        , int idcliente, int idformula, int idtipotransporte,int idtipomaterial)
        {
            var ubigeo = await _repoReadSeguimiento.GetAllUbigeo("");
            var parametersaux = new ListarTarifaOrdenParameters
            {
                idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                iddistrito = iddestino,
                idcliente = idcliente,
                idformula = idformula,
                idtipotransporte = idtipotransporte,
                idtipomaterial = idtipomaterial

            };
             var resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

            if (resultado.ToList().Count == 0)
            {
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial

                };
               resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            }
            if (resultado.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial

                };
             resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            if (resultado.ToList().Count != 0) return resultado.Where(x => x.idorigendistrito == 0 && x.idorigenprovincia == 0).ToList();
            }
            return resultado.ToList();
        }

        private async Task<List<GetAllTarifarioResult>> evaluarTarifasOrden_OrigenDistrito(int idorigen
        , int iddestino, int idcliente, int idformula, int idtipotransporte,int idtipomaterial)
        {

            var ubigeo = await _repoReadSeguimiento.GetAllUbigeo("");
            var  idorigendistrito = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddistrito;
            var parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendistrito = idorigendistrito,
                    iddistrito = iddestino,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial

                };
            var resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            //Traer provincia del distrito
            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();
            if (resultado.ToList().Count == 0)
            {
                 parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendistrito = idorigendistrito,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idtipomaterial = idtipomaterial

                };
                resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

            }
            if (resultado.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendistrito = idorigendistrito,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,

                };
                resultado = await _repoReadSeguimiento.GetListarTarifasOrden(parametersaux);

                if (resultado.ToList().Count != 0) return resultado.Where(x => x.idorigendistrito == 0 && x.idorigenprovincia == 0).ToList();
            }
            return resultado.ToList();
        }
        private static double Evaluate(string expression)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (double)(loDataTable.Rows[0]["Eval"]);
        }

        public List<CargaMasivaDetalleForRegister> ObtenerEntidades_CargaMasiva(List<List<String>> data, int idcliente)
        {
                data = validar_fin(data);
                var totales = new List<CargaMasivaDetalleForRegister>();
                CargaMasivaDetalleForRegister linea ;

                foreach (var item in data.Skip(1))
                {
                    linea =  new CargaMasivaDetalleForRegister();
                    if( idcliente == 11200)
                    {
                        linea.numguia =  ValidarRequerido(item[0] , "Documento de ventas" ); 
                        linea.waybillnum =   ValidarRequerido(item[12] , "ID Solicitante" );
                        linea.clientnum = ValidarRequerido( item[12] , "ID Solicitante") ;
                        linea.addr4 =   ValidarRequerido(item[13],"Departamento" );    // Departamento
                        linea.addr3 =  ValidarRequerido(item[14],"Provincia" ); // Provincia
                        linea.addr2 =  ValidarRequerido(item[15],"Distrito" );  // Distirito
                        linea.addr1 =  ValidarRequerido(item[16],"Dirección" );  // dirección 
                        linea.addr5 =  item[17] ;   // referencia
                        linea.homephone =   ValidarRequerido(item[18],"Celular" ); 
                        linea.busphone =  item[18];
                        linea.lastname = item[7]; // Nombre completo
                        linea.bultos = 1;
                        linea.peso =   ValidarRequerido(item[22],"Peso" );



                    }
                    else {
                        linea.daterep = ValidarRequerido(item[0] , "daterep" );
                        linea.timerep = item[1];
                        linea.ordernum = ValidarRequerido(item[2], "ordernum");
                        linea.orderdate =ValidarRequerido( item[3] , "orderdate") ;
                        linea.ordertime = item[4];
                        linea.clientnum = ValidarRequerido( item[5] , "orderdate") ;
                        linea.lastname = item[6];
                        linea.firstname =  item[7];


                        linea.addr1 = item[8];
                        linea.addr2 = item[9];
                        linea.addr3 =  item[10];
                        linea.addr4 =  item[11];
                        linea.addr5 =  item[12];

                        linea.homephone =  item[13];
                        linea.busphone =  item[14];

                        linea.postcode =  item[15];
                        linea.courier =  item[16];
                        linea.tipo =  item[17];
                        linea.peso =  item[18];
                        linea.numguia =   item[19];

                        linea.guianum =  item[20];
                        linea.fecha_real_entrega =  item[21];
                        linea.desp_dias =  item[22];
                        linea.fecha_estimada =  item[23];
                        linea.waybillnum =   ValidarRequerido(item[24] , "waybillnum" );
                        linea.pesovol = Convert.ToDecimal( item[25]);

                    }



                    totales.Add(linea);

                }
                return totales;
        }
        private string ValidarRequerido(string v, string field)
        {
            if(String.IsNullOrEmpty(v))
            {
              throw new ArgumentException( $" {field} no puede estar en blanco .");
            }
            return v;
        }
        private List<List<string>> validar_fin(List<List<string>> data)
        {
            List<List<string>> new_data = new List<List<string>>();
            foreach (var item in data)
            {
                if(item[0] == "" && item[2] == ""){
                    break;
                }
                else
                new_data.Add(item);

            }
            return new_data;
        }
         private async Task<string> SaveFile(long usuario_id)
         {

            var fullPath = string.Empty;

            var ruta =  _config.GetSection("AppSettings:Uploads").Value;

            var file = Request.Form.Files[0];
            var idOrden = usuario_id;

            string folderName = idOrden.ToString();
            string webRootPath = ruta ;
            string newPath = Path.Combine(webRootPath, folderName);

            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(Request.Form.Files[0].OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes(Request.Form.Files[0].ContentDisposition.Length);
            }

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                fullPath = Path.Combine(newPath, fileName);

                var checkextension = Path.GetExtension(fileName).ToLower();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {

                    file.CopyTo(stream);
                    //await _repoordentrabajo.SaveAll();

                }

            }
            return fullPath;

        }

        public List<List<string>> GetExcel(string fullPath)
        {
             List<List<string>> valores = new List<List<string>>();
            try
            {

                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fullPath, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();
                    // foreach (Cell cell in rows.ElementAt(0))
                    // {
                    //     dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    // }
                    foreach (Row row in rows) //this will also include your header row...
                    {
                         List<String> linea = new List<string>();
                        int columnIndex = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Gets the column index of the cell with data
                            int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                            cellColumnIndex--; //zero based index
                            if (columnIndex < cellColumnIndex)
                            {
                                do
                                {
                                    linea.Add(""); //Insert blank data here;
                                    columnIndex++;
                                }
                                while (columnIndex < cellColumnIndex);
                            }
                             linea.Add(GetCellValue(spreadSheetDocument, cell));

                            columnIndex++;
                        }
                        valores.Add(linea);
                    }
                }
               // dt.Rows.RemoveAt(0); //...so i'm taking it out here.
                     return valores;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue ==null)
            {
            return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

         public static int? GetColumnIndexFromName(string columnName)
        {

            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }
     [HttpGet("GetAllOrder")]
      public async Task<IActionResult> GetAllOrder(int? idcliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int idusuario,int? idproveedor ,int? iddestinatario, int? idtipotransporte)
      {
          var resp  = await _repoSeguimiento.GetAllOrdenTransporte(idcliente , numcp
          , fecinicio
          , fecfin , grr, docreferencia, idestado, iddestino, idusuario , iddestinatario, idtipotransporte, idproveedor);
          return Ok (resp);
      }
      [HttpGet("GetAllOrderDocument")]
      public async Task<IActionResult> GetAllOrderDocument(int? idcliente, string numcp , string fecinicio,
        string fecfin, string grr, int? idproveedor ,int? iddestinatario)
      {
          var resp  = await _repoSeguimiento.GetAllOrdenTransporteDocument(idcliente , numcp
          , fecinicio
          , fecfin , grr , iddestinatario, idproveedor);
          return Ok (resp);
      }
     [AllowAnonymous]
      [HttpGet("GetAllIncidencias")]
      public async Task<IActionResult> GetAllIncidencias(long idordentrabajo)
      {
          var resp  = await  _repoSeguimiento.GetAllIncidencias(idordentrabajo);
          return Ok(resp);
      }
     [AllowAnonymous]
    [HttpGet("GetOrden")]
    public async Task<IActionResult> GetOrden(long id)
    {

        var ot  = await  _repoordentrabajo.Get(x=>x.idordentrabajo == id);
        var resp  = await _repoSeguimiento.GetAllOrdenTransporte(null , ot.numcp
          , null
          , null , null, null, null, null, null , null, null, null);


        return Ok (resp);
    }
     [AllowAnonymous]
    [HttpGet("GetOrdenOriflame")]
    public async Task<IActionResult> GetOrdenOriflame(string id)
    {
        var resp  = await  _repoordentrabajo.Get(x=>x.docgeneral == id  && x.activo == true

        && x.idcliente == 8161 );
        return Ok (resp);
    }

    [HttpGet("GetOrdenRecojo")]
    public async Task<IActionResult> GetOrdenRecojo(long idordentrabajo)
    {
        var resp  = await _repoSeguimiento.GetOrdenRecojo(idordentrabajo);
        return Ok (resp);
    }
    [HttpGet("GetOrdenRecojoxid")]
    public async Task<IActionResult> GetOrdenRecojoxid(long idordenrecojo)
    {
        var resp  = await _repoSeguimiento.GetOrdenRecojoxid(idordenrecojo);
        return Ok (resp);
    }
     [AllowAnonymous]
    [HttpGet("getAllDocumentos")]
    public async Task<IActionResult> getAllDocumentos(int id)
    {
        var resp  = await _repoSeguimiento.GetAllDocumentos(id);
        return Ok (resp);
    }
    [HttpGet("GetAllPendienteLiquidacionxDNI")]
    public async Task<IActionResult> GetAllPendienteLiquidacionxDNI(string dni, string fec_ini, string fec_fin)
    {
        var resp  = await _repoSeguimiento.GetAllPendienteLiquidacionxDNI(dni, fec_ini, fec_fin);
        return Ok (resp);
    }


    [HttpGet("GetAllPendienteLiquidacionRepartidoresxDNI")]
    public async Task<IActionResult> GetAllPendienteLiquidacionRepartidoresxDNI(string dni, string fec_ini, string fec_fin)
    {
        var resp  = await _repoSeguimiento.GetAllPendienteLiquidacionRepartidorxDNI(dni, fec_ini, fec_fin);
        return Ok (resp);
    }






   [HttpGet("GetAllPendienteLiquidacionxDNIOT")]
    public async Task<IActionResult> GetAllPendienteLiquidacionxDNIOT(string dni, string fec_ini, string fec_fin)
    {
        var resp  = await _repoSeguimiento.GetAllPendienteLiquidacionxDNIOT(dni, fec_ini, fec_fin);
        return Ok (resp);
    }

    [HttpGet("getAllManifiestosPorEstado")]
    public async Task<IActionResult> getAllManifiestosPorEstado(int? idestado, string fec_ini, string fec_fin)
    {
        var resp = await _repoSeguimiento.getAllManifiestosPorEstado(idestado);
        return Ok(resp);
    }

    [HttpGet("getAllPendientesIngreso")]
    public async Task<IActionResult> getAllPendientesIngreso(string numcp)
    {
        var resp = await _repoSeguimiento.getAllPendientesIngresos(numcp);
        return Ok(resp);
    }
     [HttpGet("getAllPendientesDespacho")]
    public async Task<IActionResult> getAllPendientesDespacho(string numcp)
    {
        var resp = await _repoSeguimiento.getAllPendientesDespachos(numcp);
        return Ok(resp);
    }


    [HttpGet("getListarPendientesFacturacionOS")]
    public async Task<IActionResult> getListarPendientesFacturacionOS(string numhojaruta)
    {
        var resp = await _repoSeguimiento.getListarPendientesFacturacionOS(numhojaruta);
        return Ok(resp);
    }
    [HttpPost("ActualizarProveedor")]
    public async Task<IActionResult> ActualizarProveedor(CambiarProveedorCommand command )
    {
         var resp = await _repoSeguimiento.ActualizarProveedor(command);
        return Ok(resp);

    }
    [HttpPost("SetCamToOrder")]
    public async Task<IActionResult> SetCamToOrder(SetCamToOrderForUpdate command)
    {
         var resp = await _repoSeguimiento.SetCamToOrder(command);
        return Ok(resp);

    }


     [HttpPost("VincularOS")]
    public async Task<IActionResult> VincularOS(string numhojaruta , string numero)
    {

        var  manifiestos = await   _repomanifiesto.GetAll(x=> x.numhojaruta == numhojaruta);


        foreach (var manifiesto in manifiestos)
        {
            manifiesto.nrofactura =  numero;
            manifiesto.facturado = true;
        }


        await _repoordentrabajo.SaveAll();

        DateTime o  = DateTime.Now;




        return Ok (manifiestos.ToList()[0].idmanifiesto );
    }

     [HttpPost("liquidarOT")]
    public async Task<IActionResult> liquidarOT(long idordentrabajo , int idmaestroincidencia, string observacion)
    {
        var ordObj = await  _repoordentrabajo.Get(x=>x.idordentrabajo == idordentrabajo);
        ordObj.liquidacion_conductor  = true;
        ordObj.iddocumentoliq = idmaestroincidencia;
        ordObj.observacionliq = observacion;

        var ordenes = await _repoordentrabajo.GetAll(x=>x.idmanifiesto == ordObj.idmanifiesto);

        var pendientes =   ordenes.ToList().Where(x=>x.liquidacion_conductor == false || x.liquidacion_conductor == null);
        if(pendientes.Count() ==  0)
        {

            var guiaObj = await  _repomanifiesto.Get(x=>x.idmanifiesto == ordObj.idmanifiesto);
            guiaObj.idestado = 18;

        }


        await _repoordentrabajo.SaveAll();
        return Ok (ordObj);
    }

    [HttpPost("liquidarManifiesto")]
    public async Task<IActionResult> liquidarManifiesto(int idmanifiesto, int idmaestroincidencia, string observacion )
    {
        var manifiesto = await  _repomanifiesto.Get(x=>x.idmanifiesto == idmanifiesto);

        var vehiculo = await _repoVehiculo.Get(x => x.idvehiculo == manifiesto.idvehiculo);



        manifiesto.idestado = 18;
        manifiesto.iddocumentoliq = idmaestroincidencia;
        manifiesto.observacionliq = observacion;

        if(manifiesto.idtipooperacion == 123 )
        {
           manifiesto.iddestino = null;
        }



        var tarifa = await _repoReadSeguimiento.GetTarifaProveedor(manifiesto.iddestino
                                                        , vehiculo.idproveedor
                                                        , vehiculo.idtipo.Value);


        if(tarifa == 0)    {

            if(manifiesto.iddestino == null) {

            var distrito = await _repoDistrito.Get(x=>x.iddistrito == manifiesto.iddestino);


            tarifa = await _repoReadSeguimiento.GetTarifaProveedorProvincia(129
                                                        , vehiculo.idproveedor
                                                        , vehiculo.idtipo.Value);
            }
            else
            {

            var distrito = await _repoDistrito.Get(x=>x.iddistrito == manifiesto.iddestino);


            tarifa = await _repoReadSeguimiento.GetTarifaProveedorProvincia(distrito.idprovincia
                                                        , vehiculo.idproveedor
                                                        , vehiculo.idtipo.Value);

            }

        }

        manifiesto.costoproveedor = tarifa;


        var ordenes = await _repoordentrabajo.GetAll(x=>x.idmanifiesto == idmanifiesto);

        foreach (var item in ordenes.ToList())
        {
           item.liquidacion_conductor = true;
        }

        await _guiaremisionblanco.SaveAll();
        return Ok (manifiesto);
    }


    [HttpPost("asignarGuiasBlanco")]
    public async Task<IActionResult> asignarGuiasBlanco(int guia, long id )
    {
        var guiaObj = await _guiaremisionblanco.Get(x=>x.id == guia);
        guiaObj.idordentrabajo = id;
        guiaObj.idestado = 2;
        await _guiaremisionblanco.SaveAll();
        return Ok (guiaObj);
    }
    [HttpPost("asignarGuiasBlancoExtraviado")]
    public async Task<IActionResult> asignarGuiasBlancoExtraviado(int guia, long id )
    {
        var guiaObj = await _guiaremisionblanco.Get(x=>x.id == guia);
        guiaObj.idordentrabajo = id;
        guiaObj.idestado = 3;
        await _guiaremisionblanco.SaveAll();
        return Ok (guiaObj);
    }
    [HttpPost("desvincularGuiasBlanco")]
    public async Task<IActionResult> desvincularGuiasBlanco(int guia, long id )
    {
        var guiaObj = await _guiaremisionblanco.Get(x=>x.id == guia && x.idordentrabajo == id ) ;
        guiaObj.idordentrabajo = null;
        guiaObj.idestado = 1;
        await _guiaremisionblanco.SaveAll();
        return Ok (guiaObj);
    }

    [HttpGet("getAllManifiestoPendientes")]
    public async Task<IActionResult> getAllManifiestoPendientes (string numhojaruta)
    {
        var resp  = await _repoSeguimiento.getAllManifiestoPendientes(numhojaruta);
        return Ok (resp);
    }
    [HttpGet("getAllOrdersxManifiesto")]
    public async Task<IActionResult> getAllOrdersxManifiesto (int idmanifiesto)
    {
        var resp  = await _repoSeguimiento.getAllOrdersxManifiesto(idmanifiesto);
        return Ok (resp);
    }

    [HttpGet("GetAllPendienteLiquidacionxManifiesto")]
    public async Task<IActionResult> GetAllPendienteLiquidacionxManifiesto(int idmanifiesto)
    {
        var resp  = await _repoSeguimiento.GetAllPendienteLiquidacionesxManifiesto(idmanifiesto);
        return Ok (resp);
    }
      [AllowAnonymous]
      [HttpGet("GetAllOrderOtros")]
      public async Task<IActionResult> GetAllOrderOtros(string guiarecojo , string numcp, string clave)
      {
          var resp  = await _repoSeguimiento.GetAllOrdenTransporteGeneral( numcp, guiarecojo, clave );
          return Ok (resp.FirstOrDefault());
      }

      [HttpGet("GetAllClients")]
      public async Task<IActionResult> GetAllClients(string idscliente)
      {
          if( idscliente == "null") idscliente = null;
          var resp  = await _repoSeguimiento.GetAllClientes(idscliente);
          return Ok (resp);
      }
      [HttpGet("GetAllDestinatarios")]
      public async Task<IActionResult> GetAllDestinatarios(int idcliente)
      {
          var resp  = await _repoSeguimiento.GetAllDestinatarios(idcliente);
          return Ok (resp);
      }


      [HttpGet("GetAllPorEstado")]
      public async Task<IActionResult> GetAllPorEstado(int? idcliente, int? iddestino )
      {
          var resp  = await _repoReadSeguimiento.GetPorEstado(idcliente,iddestino);
          return Ok (resp);
      }
      [HttpGet("GetDespachosATiempo")]
      public async Task<IActionResult> GetDespachosATiempo(int? idcliente, string fec_ini, string fec_fin)
      {
            var result = await _repoReadSeguimiento.GetDespachosATiempo(idcliente, fec_ini,fec_fin);
            return Ok (result);
      }
      [HttpGet("GetRetornoDocumentario")]
      public async Task<IActionResult> GetRetornoDocumentario(int? idcliente, string fec_ini, string fec_fin)
      {
            var result = await _repoReadSeguimiento.GetEntregaVsConciliacion(idcliente, fec_ini,fec_fin);
            return Ok (result);
      }
      [HttpGet("GetConciliacionDocumentaria")]
      public async Task<IActionResult> GetConciliacionDocumentaria(int? idcliente
      , string fec_ini, string fec_fin, int? iddepartamento, int? idprovincia)
      {
            var result = await _repoReadSeguimiento.GetPendientesRetornoDocumentario(idcliente, fec_ini,fec_fin, iddepartamento, idprovincia);
            return Ok (result);
      }

      [HttpGet("GetPendientesDespacho")]
      public async Task<IActionResult> GetPendientesDespacho(int? idcliente
      , string fec_ini, string fec_fin, int? iddepartamento, int? idprovincia)
      {
            var result = await _repoReadSeguimiento.GetPendientesDespacho(idcliente, fec_ini,fec_fin, iddepartamento, idprovincia);
            return Ok (result);
      }
      [HttpGet("GetPendientesEntrega")]
      public async Task<IActionResult> GetPendientesEntrega(int? idcliente
      , string fec_ini, string fec_fin, int? iddepartamento, int? idprovincia)
      {
            var result = await _repoReadSeguimiento.GetPendientesEntrega(idcliente, fec_ini,fec_fin, iddepartamento, idprovincia);
            return Ok (result);
      }


      [HttpGet("GetListUbigeo")]
      public async Task<IActionResult> GetListUbigeo(string criterio)
      {
          var resp  = await _repoSeguimiento.GetListarUbigeo(criterio);
          return Ok (resp);
      }

      [AllowAnonymous]
      [HttpGet("GetListFiles")]
      public async Task<IActionResult> GetListFiles(long? archivoid, long? ordenid)
      {
          var resp  = await _repoSeguimiento.GetListarArchivos(archivoid, ordenid);
          return Ok (resp);
      }
        [AllowAnonymous]
      [HttpGet("GetListGuias")]
      public async Task<IActionResult> GetListGuias(long? ordenid)
      {
          var resp  = await _repoSeguimiento.GetListarGuias(ordenid);
          return Ok (resp);
      }
        [AllowAnonymous]
      [HttpGet("DownloadArchivo")]
      public FileResult DownloadArchivo(long documentoId)
      {
            string filePath =   _config.GetSection("AppSettings:UploadsDocuments").Value;

            var documento = _archivo.Get(x=>x.idarchivo == documentoId).Result;

            IFileProvider provider = new PhysicalFileProvider(documento.rutaacceso );
            IFileInfo fileInfo = provider.GetFileInfo(documento.nombrearchivo);
            var readStream = fileInfo.CreateReadStream();
            //var mimeType = "application/vnd.ms-excel";
            return File(readStream,GetContentType(documento.rutaacceso + "//" + documento.nombrearchivo) , documento.nombrearchivo);

       }
        private string GetContentType(string path)
        {
           var provider = new FileExtensionContentTypeProvider();
           string contentType;
           if(!provider.TryGetContentType(path, out contentType))
           {
               contentType = "application/octet-stream";
           }
           return contentType;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegistrarOrdenRecojo(OrdenRecojoRegister orden )
        {
           var resp  = await _repoSeguimiento.RegistrarOrdenRecojo(orden);
           orden.numcp  = "OR-" + resp.ToString().PadLeft(6,'0');
            return Ok (  orden );
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateOrdenRecojo(OrdenRecojoRegister orden )
        {
           var resp  = await _repoSeguimiento.UpdateOrdenRecojo(orden);
            return Ok (  orden );
        }

        [HttpPost("DeleteOrdenRecojo")]
        public async Task<IActionResult> DeleteOrdenRecojo(OrdenRecojoDelete orden )
        {
           //var resp  = await _repoSeguimiento.UpdateOrdenRecojo(orden);
            var ordenDb = await  _repoordentrabajo.Get(x=>x.idordentrabajo == orden.idordentrabajo);
            ordenDb.activo = false;
            await _repoordentrabajo.SaveAll();

            return Ok (  orden );
        }

        [HttpGet("GetAllValorTabla")]
        public async Task<IActionResult> GetAllValorTabla(int TablaId)
        {
                var resp  = await _repoReadSeguimiento.GetAllValorTabla(TablaId);
                return Ok (resp);

        }
        [HttpGet("GetAllOrdenesTrabajoXids")]
        public async Task<IActionResult> GetAllOrdenesRecojo(string ids)
        {
                var resp  = await _repoReadSeguimiento.GetAllOrdenesTrabajoxIds(ids);
                return Ok (resp);

        }
        [HttpGet("GetAllOrdenesRecojo")]
        public async Task<IActionResult> GetAllOrdenesRecojo(int? idcliente, string fec_ini, string fec_fin, int? idestado)
        {
                var resp  = await _repoReadSeguimiento.GetAllOrdenesRecojo(idcliente,fec_ini,fec_fin, idestado);
                return Ok (resp);

        }
        [HttpGet("GetAllPlacasProgramadas")]
        public async Task<IActionResult> GetAllPlacasProgramadas(string ruc, string placa)
        {
                var resp  = await _repoReadSeguimiento.GetAllPlacasProgramadas(ruc, placa);
                return Ok (resp);

        }
        [HttpGet("GetEquipoTransporteAsociado")]
        public async Task<IActionResult> GetEquipoTransporteAsociado(long idor)
        {
              var resp  = await _repoReadSeguimiento.GetEquipoTransporteAsociado(idor);
              return Ok (resp);
        }

        [HttpGet("GetEquipoTransporte")]
        public async Task<IActionResult> GetEquipoTransporte(string placa)
        {
              var resp  = await _repoReadSeguimiento.GetEquipoTransporte(placa);
              return Ok (resp);
        }
        [HttpDelete("DeleteCuadrilla")]
        public async Task<IActionResult> DeleteCuadrilla(long id)
        {
             var cuadrilla = await _cuadrilla.Get(x=>x.id == id );
             _cuadrilla.Delete(cuadrilla);

            await _cuadrilla.SaveAll();
            return Ok(cuadrilla);
        }
        [HttpPost("GuardarCuadrilla")]
        public async Task<IActionResult> GuardarCuadrilla(CuadrillaForRegisterDto cuadrillaForRegisterDto)
        {
            var cuadrilla =  new Cuadrilla();
            cuadrilla.dni =       cuadrillaForRegisterDto.dni;
            cuadrilla.nombrecompleto  = cuadrillaForRegisterDto.nombrecompleto;
            cuadrilla.idordenrecojo = cuadrillaForRegisterDto.idordenrecojo;

            var resp = await _cuadrilla.AddAsync(cuadrilla);
            await _cuadrilla.SaveAll();
            return Ok(cuadrilla);
        }
        [HttpPost("RegisterEquipoTransporte")]
        public async Task<IActionResult> RegisterEquipoTransporte(EquipoTransporteForRegister equipotrans)
        {
              List<long> ids  = new List<long>();
              String[] prm = equipotrans.ids.Split(',');
              OrdenTrabajo obj ;

              var idmanifiesto =
                  await _repoSeguimiento.RegistarManifiesto(equipotrans.idvehiculo, equipotrans.idusuarioregistro);




              foreach (var item in prm)
              {
                   if(item == "undefined") continue;
                   obj =  await  _repoordentrabajo.Get(x=>x.idordentrabajo ==  Convert.ToInt64(item));
                   obj.idvehiculo = equipotrans.idvehiculo;
                   obj.idproveedor = equipotrans.idproveedor;
                   obj.idchofer = equipotrans.idchofer;
                   obj.idestado = 27; // programado
                   obj.idmanifiesto = idmanifiesto;
              }

                try
                {
                    var createdEquipoTransporte =  await _repoordentrabajo.SaveAll();
                    return Ok(idmanifiesto);
                }
                catch (System.Exception)
                {

                    throw;
                }

        }
        [HttpGet("GetListarCalendario")]
        public async Task<IActionResult> GetListarCalendario()
        {
            var resp  = await _repoSeguimiento.GetListarCalendario();
            return Ok (resp);
        }



        [HttpPost("RegistroGuiaRemisionBlanco")]
        public async Task<IActionResult> RegistroGuiaRemisionBlanco(GuiaRemisionBlancoForRegister guiaregister)
        {
           var result = await _repoSeguimiento.RegistroGuiaRemisionBlanco(guiaregister);
            return Ok(result);
        }
        [HttpPost("EliminarGuiaRemisionBlanco")]
        public async Task<IActionResult> EliminarGuiaRemisionBlanco(long id)
        {
           var result = await _repoSeguimiento.EliminarGuiaRemisionBlanco(id);
            return Ok(result);
        }

        [HttpGet("GetGuiaRemisionBlancoPorVehiculo")]
        public async Task<IActionResult> GetGuiaRemisionBlancoPorVehiculo(int idmanifiesto, long idordentrabajo)
        {
            var result = await _repoSeguimiento.GetGuiaRemisionBlancoPorVehiculo(idmanifiesto, idordentrabajo);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteGuiaRemisionBlanco(int id )
        {
            var guiaDb = await  _guiaremisionblanco.Get(x=>x.id == id);
            _guiaremisionblanco.Delete(guiaDb);
            await _guiaremisionblanco.SaveAll();
            return Ok (  guiaDb );
        }


        [HttpGet("GetAllCuadrilla")]
        public async Task<IActionResult> GetAllCuadrilla(long idrecojo)
        {
              var resp  = await _repoReadSeguimiento.GetAllCuadrilla(idrecojo);
              return Ok (resp);
        }
        [HttpPost("ConfirmarLiquidacionDoc")]
        public async Task<IActionResult> ConfirmarLiquidacionDoc(InsertarActualizarEtapaCommand command)
        {

            try
            {
                var model = new LiquidacionForUpdateDto();


                var hm = command.horaentrega.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);

                model.fechaentregaconciliacion = command.fechaentrega.Value.Date + ts;
                model.archivado = true;
                model.idusuarioconciliacion = Convert.ToInt32(command.idusuarioentrega);
                model.idestado = (int)Constantes.EstadoOT.PendienteFacturacion;
                model.idordentrabajo = command.idordentrabajo.Value;

                await _repoSeguimiento.ConfirmarRetornoDocumentario(model);    
                return Ok();

            }
            catch (System.Exception ex)
            {
                throw new ArgumentException("Error. Datos Incorrectos");
            }

        
          


        }


        [HttpPost("ConfirmarEntregav2")]
        public async Task<IActionResult> ConfirmarEntregav2(InsertarActualizarEtapaCommand model)
        {
            var ordentrabajo = await  _repoordentrabajo.Get(x=>x.idordentrabajo== model.idordentrabajo);

            if(model.enzona.HasValue)
            {
                ordentrabajo.enzona = model.enzona;
                await _repoordentrabajo.SaveAll();
            }

            if(model.horaentrega == null || model.fechaentrega == null)
            {
                if(model.enzona == true)
                  throw new ArgumentException("Faltan datos para confirmar, registrado en zona. "  );
                else 
                  throw new ArgumentException("Faltan datos para confirmar");
            }

          

            model.visible = true;

            var hm = model.horaentrega.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);

            model.fechaetapa = model.fechaentrega.Value.Date + ts;
            model.fecharegistro = DateTime.Now;
            model.idestado = (Int16) Constantes.EstadoOT.PendienteRetornoDocumentario;
            model.idusuarioregistro =  model.idusuarioentrega;
            model.canal_confirmacion_id = 2;
            model.incidenciaentregaid = model.idtipoentrega;
            model.documento = model.dnientrega;
            model.recurso = model.personaentrega;
            
            model.enzona = true;
     

            await _repoSeguimiento.ConfirmarEntrega(model);


             return Ok(model);
        }

        [HttpPost("confirmarVisitas")]
        public async Task<IActionResult> confirmarVisitas(VisitasRegisterDto visitasRegisterDto )
        {

                var orden = await  _repoordentrabajo.Get(x => x.idordentrabajo == visitasRegisterDto.idordentrabajo);
                orden.fecvisita1 = visitasRegisterDto.fecvisita1;
                orden.motivo1 = visitasRegisterDto.motivo1;

                orden.fecvisita2 = visitasRegisterDto.fecvisita2;
                orden.motivo2 = visitasRegisterDto.motivo2;

                orden.fecvisita3 = visitasRegisterDto.fecvisita3;
                orden.motivo3 = visitasRegisterDto.motivo3;
                orden.observacionvisita = visitasRegisterDto.observacionvisita;


            await _repoordentrabajo.SaveAll();




            return Ok (  orden );
        }
         [HttpGet("GetListarSustentoxHR")]
         public async Task<IActionResult> GetListarSustentoxHR(string hojaruta)
        {
            var resp  = await _repoSeguimiento.GetListarSustentoxHR(hojaruta);
            return Ok (resp);
        }
        [HttpGet("GetSustentoForHojaRuta")]
        public async Task<IActionResult> GetAllTipoSustento(string hojaruta)
        {
            var resp = await  _Sustento.Get(x => x.numhojaruta ==  hojaruta);
            return Ok(resp);
        }

        [HttpPost("InsertSustento")]
        public async Task<IActionResult> InsertSustento(SustentoForRegisterDto sustentoForRegister)
        {
            Sustento sustento = new Sustento();
            sustento.fecha = sustentoForRegister.fecha;
            sustento.idusuarioregistro = sustentoForRegister.idusuarioregistro;
            sustento.kilometrajefinal = sustentoForRegister.kilometrajefinal;
            sustento.kilometrajeInicio = sustentoForRegister.kilometrajeInicio;
            sustento.montodepositado = sustentoForRegister.montodepositado;
            sustento.numhojaruta = sustentoForRegister.numhojaruta;
            sustento.idestado = 26;


            var resp = await  _Sustento.AddAsync(sustento);
            await _Sustento.SaveAll();

            return Ok(resp);
        }
        [HttpPost("InsertSustentoDetalle")]
        public async Task<IActionResult> InsertSustentoDetalle(SustentoDetalleForRegisterDto sustentoForRegister)
        {
            SustentoDetalle sustento = new SustentoDetalle();
            sustento.sustentoid = sustentoForRegister.sustentoid;
            sustento.fecha = sustentoForRegister.fecha;
            sustento.idtiposustento = sustentoForRegister.idtiposustento;
            sustento.idtipodocumento = sustentoForRegister.idtipodocumento;
            sustento.numeroDocumento = sustentoForRegister.numeroDocumento;
            sustento.montoBase = sustentoForRegister.montoBase;
            sustento.montoImpuesto = Convert.ToDecimal(Convert.ToDouble(sustento.montoBase) * Convert.ToDouble(0.18));
            sustento.montoTotal = sustento.montoBase + sustento.montoImpuesto;
            sustento.fechaRegistro = DateTime.Now;

            var resp = await  _SustentoDetalle.AddAsync(sustento);
            await _SustentoDetalle.SaveAll();

            return Ok(resp);
        }
        [HttpPost("LiquidarHojaRuta")]
        public async Task<IActionResult> LiquidarHojaRuta(string hojaruta)
        {
            var resp = await  _Sustento.Get(x => x.numhojaruta ==  hojaruta);
            resp.idestado = 28;

            await _Sustento.SaveAll();

            return Ok(resp);
        }

    }
}

