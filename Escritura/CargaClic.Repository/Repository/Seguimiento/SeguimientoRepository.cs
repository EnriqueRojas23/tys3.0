
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CargaClic.API.Dtos.Seguimiento;
using CargaClic.Data;
using CargaClic.Domain.Mantenimiento;
using CargaClic.Domain.Seguimiento;
using CargaClic.Repository.Contracts.Seguimiento;
using CargaClic.Repository.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using Webapi.Data.Domain;
using Webapi.Dtos;

namespace CargaClic.Repository
{
    public class SeguimientoRepository : ISeguimientoRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public SeguimientoRepository(DataContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IDbConnection Connection
        {   
            get
            {
                var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                try
                {
                     connection.Open();
                     return connection;
                }
                catch (System.Exception)
                {
                    connection.Close();
                    connection.Dispose();
                    return connection;
                   // throw;
                }
            }
        }

         public async Task<IEnumerable<ListarClientesResult>> GetAllClientes(string clientesids)
        {
            var parametros = new DynamicParameters();
            parametros.Add("clientesids", dbType: DbType.String, direction: ParameterDirection.Input, value: @clientesids);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarclientes_nuevo]";
                var result = await conn.QueryAsync<ListarClientesResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }
         public async Task<IEnumerable<GetDocumentoResult>> GetAllDocumentos(int id)
        {
           var parametros = new DynamicParameters();
            parametros.Add("idarchivo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: null);
            parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: id);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[liquidacion].[pa_listararchivos]";
                var result = await conn.QueryAsync<GetDocumentoResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }

       

        public async Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporte(int? idcliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int? idusuario, int? iddestinatario, int? idtipo, int? idproveedor, string guiarecojo, string clave)
        {
            var parametros = new DynamicParameters();

            parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idcliente);
            parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: numcp);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fecinicio);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fecfin);
            parametros.Add("grr", dbType: DbType.String, direction: ParameterDirection.Input, value: grr);
            parametros.Add("docgeneral", dbType: DbType.String, direction: ParameterDirection.Input, value: docreferencia);
            parametros.Add("idestado", dbType: DbType.Int16, direction: ParameterDirection.Input, value: idestado);
            parametros.Add("iddestino", dbType: DbType.Int16, direction: ParameterDirection.Input, value: iddestino);
            parametros.Add("idusuario", dbType: DbType.Int16, direction: ParameterDirection.Input, value: idusuario);
            parametros.Add("guiarecojo", dbType: DbType.String, direction: ParameterDirection.Input, value: guiarecojo);
            parametros.Add("iddestinatario", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddestinatario);
            parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idtipo);
            parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idproveedor);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarordentrabajo_cliente_v3]";
                var result = await conn.QueryAsync<GetAllOrdenTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.ToList().OrderByDescending(x=>x.fecharegistro);
            }
        }

        public async Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporteGeneral(string numcp, string guiarecojo, string clave = "")
        {
             var parametros = new DynamicParameters();

            parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: numcp);
            parametros.Add("guiarecojo", dbType: DbType.String, direction: ParameterDirection.Input, value: guiarecojo);
            parametros.Add("clave", dbType: DbType.String, direction: ParameterDirection.Input, value: clave);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarordentrabajo_cliente_v3.1]";
                var result = await conn.QueryAsync<GetAllOrdenTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<IEnumerable<ListarArchivosResult>> GetListarArchivos(long? idarchivo, long? idorden)
        {

            var parametros = new DynamicParameters();
            parametros.Add("idarchivo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idarchivo);
            parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idorden);

            using(IDbConnection conn = Connection)
            {
                var resultado = await conn.QueryAsync<ListarArchivosResult>
                    (
                       "liquidacion.pa_listararchivos",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    );
                return resultado;
            }
        }

        public async Task<IEnumerable<ListarGuiasResult>> GetListarGuias(long? idorden)
        {
            var parametros = new DynamicParameters();
            parametros.Add("idorden", dbType: DbType.String, direction: ParameterDirection.Input, value: idorden);
            
            using (IDbConnection conn = Connection)
            {
                  var  result = await conn.QueryAsync<ListarGuiasResult>
                                    (
                                        "seguimiento.pa_listarguias",
                                        parametros,
                                        commandType: CommandType.StoredProcedure
                                    );
                return result;
            }
        }

        public async Task<IEnumerable<ListarUbigeoResult>> GetListarUbigeo(string criterio)
        {
            var parametros = new DynamicParameters();
            parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: criterio);
            
            using (IDbConnection conn = Connection)
            {
                  var  result = await conn.QueryAsync<ListarUbigeoResult>
                                    (
                                        "seguimiento.pa_listarubigeo",
                                        parametros,
                                        commandType: CommandType.StoredProcedure
                                    );
                return result;
            }
        }   
        public async Task<IEnumerable<IncidenciaResult>> GetAllIncidencias(long idordentrabajo)
        {
             var parametros = new DynamicParameters();
            // parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: null);
            // parametros.Add("idmaestroetapa", dbType: DbType.String, direction: ParameterDirection.Input, value: null);
            // parametros.Add("idmaestroincidencia", dbType: DbType.String, direction: ParameterDirection.Input, value: null);
            parametros.Add("idorden", dbType: DbType.String, direction: ParameterDirection.Input, value: idordentrabajo);


            using (IDbConnection conn = Connection)
            {
                  var  result = await conn.QueryAsync<IncidenciaResult>
                                    (
                                        "monitoreo.pa_listarincidencias",
                                        parametros,
                                        commandType: CommandType.StoredProcedure
                                    );
                return result;
            }
        }

        public async Task<long> RegistrarOrdenRecojo(OrdenRecojoRegister command)
        {
             OrdenRecojo dominioOr = null;
             OrdenTrabajo dominioOt = null;
   
             using(var transaction = _context.Database.BeginTransaction())
             {
                 dominioOt = new OrdenTrabajo();

                 dominioOt.idcliente = command.idcliente.Value;
                 dominioOt.dnipersonarecojo = command.dnipersonarecojo;
                 dominioOt.personarecojo = command.personarecojo;
                 dominioOt.fecharegistro = DateTime.Now;
                 dominioOt.puntopartida = command.puntopartida;
                 dominioOt.numcp = "OR-00001";
                 dominioOt.tipoorden = 2;
                 dominioOt.peso = command.peso;
                 dominioOt.volumen = command.volumen;
                 dominioOt.bulto = command.bulto;
                 dominioOt.idestado = 26;
                 dominioOt.activo = true;
                
                

                try
                {
                    _context.OrdenTrabajo.Add(dominioOt);
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                 dominioOr = new OrdenRecojo();
                 dominioOr.centroacopio = command.centroacopio;
                 dominioOr.fechahoracita = Convert.ToDateTime(command.fechacita.ToShortDateString() + ' ' +  command.horacita);
                 dominioOr.idordentrabajo = dominioOt.idordentrabajo;
                 dominioOr.idtipounidad = command.idtipounidad;
                 dominioOr.observaciones= command.observaciones;
                 dominioOr.responsablecomercialid = command.responsablecomercialid.Value;
                //  dominioOr.idestadorecojo = 26; //Registrado
                
                 dominioOr.observaciones = command.observaciones;
                
              

                try
                {
                    _context.OrdenRecojo.Add(dominioOr);
                    await _context.SaveChangesAsync();
                    
                    dominioOt.numcp = "OR-" + dominioOr.idordenrecojo.ToString().PadLeft(6,'0');
                     await _context.SaveChangesAsync();

                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                
                
                transaction.Commit();
                return dominioOr.idordenrecojo;

             }
        }
        
        public async Task<GetAllOrdenTransporteResult> GetOrdenRecojoxid(long idordenrecojo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("idordenrecojo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idordenrecojo);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtenerordenrecojo_x_id]";
                var result = await conn.QueryAsync<GetAllOrdenTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.SingleOrDefault();
            }
        }

        public async Task<GetAllOrdenTransporteResult> GetOrdenRecojo(long idordentrabajo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("idordentransporte", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idordentrabajo);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtenerordenrecojo]";
                var result = await conn.QueryAsync<GetAllOrdenTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.SingleOrDefault();
            }
        }

        public async Task<long> UpdateOrdenRecojo(OrdenRecojoRegister command)
        {
             OrdenRecojo dominioOr =   _context.OrdenRecojo.Where(x=>x.idordenrecojo == command.idordenrecojo).SingleOrDefault();
             OrdenTrabajo dominioOt = _context.OrdenTrabajo.Where(x=>x.idordentrabajo == command.idordentrabajo).SingleOrDefault();;
   
             using(var transaction = _context.Database.BeginTransaction())
             {
                 

                 dominioOt.idcliente = command.idcliente.Value;
                 dominioOt.dnipersonarecojo = command.dnipersonarecojo;
                 dominioOt.personarecojo = command.personarecojo;
                 dominioOt.fecharegistro = DateTime.Now;
                 dominioOt.puntopartida = command.puntopartida;
                 dominioOt.tipoorden = 2;
                 dominioOt.peso = command.peso;
                 dominioOt.volumen = command.volumen;
                 dominioOt.bulto = command.bulto;
                

                try
                {
                    
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                 
                 dominioOr.centroacopio = command.centroacopio;
                 dominioOr.fechahoracita = Convert.ToDateTime(command.fechacita.ToShortDateString() + ' ' +  command.horacita);
                 dominioOr.idordentrabajo = dominioOt.idordentrabajo;
                 dominioOr.idtipounidad = command.idtipounidad;
                 dominioOr.observaciones= command.observaciones;
                 dominioOr.responsablecomercialid = command.responsablecomercialid.Value;
                //  dominioOr.idestadorecojo = 26; //Registrado
                 //dominioOr.observaciones = command.observaciones;
              

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                
                
                transaction.Commit();
                return dominioOr.idordenrecojo;

             }
        }

        public async Task<IEnumerable<CalendarioResult>> GetListarCalendario()
        {
             var parametros = new DynamicParameters();
            // parametros.Add("idordentransporte", dbType: DbType.String, direction: ParameterDirection.Input, value: idordentrabajo);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listar_calendario]";
                var result = await conn.QueryAsync<CalendarioResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionxDNI(string dni, string fec_ini, string fec_fin)
        {
             var parametros = new DynamicParameters();
            parametros.Add("dni", dbType: DbType.String, direction: ParameterDirection.Input, value: dni);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarpendienteliquidacionxDNI_agrupado]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }
         public async Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionRepartidorxDNI(string dni, string fec_ini, string fec_fin)
        {
             var parametros = new DynamicParameters();
            parametros.Add("dni", dbType: DbType.String, direction: ParameterDirection.Input, value: dni);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarpendienteliquidacionRepartidorxDNI]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }
         public async Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionxDNIOT(string dni,string fec_ini, string fec_fin)
        {
             var parametros = new DynamicParameters();
            parametros.Add("dni", dbType: DbType.String, direction: ParameterDirection.Input, value: dni);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarpendienteliquidacionxDNI_agrupadoOT]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }
         public async Task<IEnumerable<GuiaRemisionBlanco>> RegistroGuiaRemisionBlanco(GuiaRemisionBlancoForRegister guia)
        {
            GuiaRemisionBlanco  obj;
            List<GuiaRemisionBlanco> objs = new List<GuiaRemisionBlanco>();

            string[] prm = guia.numeroguia.Split('-');

            

            try {

                if (guia.cantidad > 0 && prm[0].Length ==3 && prm[1].Length < 8){

                    long valor = Convert.ToUInt32(prm[1].ToString()); 
                     
                    using(var transaction = _context.Database.BeginTransaction())
                    {

                            
                            try
                            {
                                for (int i = 0; i < guia.cantidad; i++)
                                {
                                    obj = new GuiaRemisionBlanco();
                                    obj.numeroguia = prm[0].ToString() + '-' + string.Format("{0:0000000}", valor + i);
                                    obj.idvehiculo = guia.idvehiculo;          
                                    obj.idmanifiesto = guia.idmanifiesto; 
                                    obj.fecharegistro = DateTime.Now;   
                                    obj.idestado = 1;

                                    if( _context.guiaRemisionBlancos.Where(x=>x.numeroguia == obj.numeroguia && x.idestado == 2 ).ToList().Count > 0 )
                                    {
                                        return null;
                                    }


                                    objs.Add(obj);     

                                }  
                                
                                await  _context.guiaRemisionBlancos.AddRangeAsync(objs);
                                await _context.SaveChangesAsync();
                                transaction.Commit();

                            }
                            catch (System.Exception ex)
                            {
                                    transaction.Rollback(); 
                                    var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;
                                    throw new ArgumentException("Error al insertar");
                            }
                            return objs;                       
                        
                    }
                } 
                else return null;
            }
            catch{return null;}
        }

        public async Task<IEnumerable<ListaGuiaRemisionBlancoResult>> GetGuiaRemisionBlancoPorVehiculo(int idmanifiesto, long idordentrabajo)
        {
            using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("idmanifiesto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idmanifiesto);
                parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idordentrabajo);
                var result = await conn.QueryAsync<ListaGuiaRemisionBlancoResult>("[seguimiento].[pa_guiaremisionblancoporvehiculo]",
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                );
                return result;
            }
          
        }

        public async Task<long> RegistarManifiesto(int idvehiculo, int idusuarioregistro)
        {
            string numerohojaruta ;
            string numeromanifiesto ;
             var parametros = new DynamicParameters();
             using (IDbConnection conn = Connection)
             {

                    
                   var  resultado_1 = conn.Query<ObtenerUltimoManifiestoResult>
                        (
                            "seguimiento.pa_obtenerultimomanifiesto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                   numeromanifiesto =  resultado_1.nummanifiesto.Split('-')[0].ToString() + "-" 
                        + (Convert.ToInt32(resultado_1.nummanifiesto.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');

                     
                     var resultado = conn.Query<ObtenerUltimaHojaRutaResult>
                        (
                            "seguimiento.pa_obtenerultimohojaruta",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault(); 

                         numerohojaruta = resultado.numhojaruta.Split('-')[0].ToString() + "-" 
                         + (Convert.ToInt32(resultado.numhojaruta.Split('-')[1].ToString()) + 1)
                                        .ToString().PadLeft(6, '0');
             }
             var manifiesto = new Manifiesto();
             manifiesto.activo = true;
             manifiesto.fecharegistro = DateTime.Now;
             manifiesto.idtipooperacion = 0;
             manifiesto.idusuarioregistro = idusuarioregistro;
             manifiesto.idvehiculo = idvehiculo;
             manifiesto.numhojaruta = numerohojaruta;
             manifiesto.nummanifiesto = numeromanifiesto;
             manifiesto.idestado  = 16;

             await _context.Manifiesto.AddAsync(manifiesto);
             await _context.SaveChangesAsync();

             return manifiesto.idmanifiesto;
        }

        public async Task<long> EliminarGuiaRemisionBlanco(long id)
        {
              var guia =  _context.guiaRemisionBlancos.Where(x=>x.id == id).Single();
               _context.guiaRemisionBlancos.Remove(guia);
              await _context.SaveChangesAsync(); 
              return 1;
        }

        public async Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionesxManifiesto(int idmanifiesto)
        {
             var parametros = new DynamicParameters();
            parametros.Add("idmanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: idmanifiesto);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarpendienteliquidacionxManifiestoOR]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }

        public async Task<IEnumerable<PendientesLiquidacionResult>> getAllManifiestoPendientes(string hojaruta)
        {
             var parametros = new DynamicParameters();
            parametros.Add("hojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: hojaruta);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtenermanifiestos_hojaruta]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }

        public async Task<IEnumerable<PendientesLiquidacionResult>> getAllOrdersxManifiesto(int idmanifiesto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("idmanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: idmanifiesto);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[monitoreo].[pa_listarordenesxmanifiesto_2]";
                var result = await conn.QueryAsync<PendientesLiquidacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 
                return result;
            }
        }

        public async Task<IEnumerable<ListarProveedorxClienteDto>> GetAllDestinatarios(int idcliente)
        {
            using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idcliente);

                
                var resultado = await conn.QueryAsync<ListarProveedorxClienteDto>
                    (
                        "seguimiento.pa_listarproveedorxcliente",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
            

                return resultado;
            }
        }

        public async Task<IEnumerable<ManifiestoxEstados>> getAllManifiestosPorEstado(int? idestado)
        {
           using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idestado);

                
                var resultado = await conn.QueryAsync<ManifiestoxEstados>
                    (
                        "seguimiento.pa_listar_liquidacionmanifiestos",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
            

                return resultado;
            }
        }

        public async Task<IEnumerable<DetalleOT>> getAllPendientesIngresos(string numcp)
        {
             using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: numcp);

                
                var resultado = await conn.QueryAsync<DetalleOT>
                    (
                        "seguimiento.pa_listar_pendienteingreso_otdetalle",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
            

                return resultado;
            }
        }

        public async Task<IEnumerable<DetalleOT>> getAllPendientesDespachos(string numcp)
        {
             using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: numcp);

                
                var resultado = await conn.QueryAsync<DetalleOT>
                    (
                        "seguimiento.pa_listar_pendientedespacho_otdetalle",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
            

                return resultado;
            }
        }
        public async Task<int> ActualizarProveedor(CambiarProveedorCommand command )
        {
            using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("idordentrabajo", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idordentrabajo);
                parametros.Add("idproveedor", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idproveedor);

                
                var resultado = await conn.QueryAsync<DetalleOT>
                    (
                        "seguimiento.pa_cambiarrepartidor",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
            

                return 1;
            }
        }
      public async Task<int> RegisterCargaMasiva(CargaMasivaForRegister command, IEnumerable<CargaMasivaDetalleForRegister> commandDetais )
        {
            CargaMasivaDetalle cargaMasivaDetalle ;
            CargaMasiva cargaMasiva = new CargaMasiva(); 
            cargaMasiva.estadoid = 1;
            cargaMasiva.fecharegistro = DateTime.Now;
            cargaMasiva.usuarioid  = 1; 

            List<CargaMasivaDetalle> cargaMasivaDetalles = new List<CargaMasivaDetalle>();

            using(var transaction = _context.Database.BeginTransaction())
            {

                await _context.AddAsync<CargaMasiva>(cargaMasiva);
                await _context.SaveChangesAsync();

                foreach (var item in commandDetais)
                {
                    cargaMasivaDetalle = new CargaMasivaDetalle();
                    cargaMasivaDetalle.addr1 = item.addr1.Trim();;
                    cargaMasivaDetalle.cargaid = cargaMasiva.id;
                    cargaMasivaDetalle.addr2 = item.addr2.Trim();
                    cargaMasivaDetalle.addr3 = item.addr3.Trim();
                    cargaMasivaDetalle.addr4 = item.addr4.Trim();
                    cargaMasivaDetalle.addr5 = item.addr5;
                    cargaMasivaDetalle.busphone = item.busphone;
                    cargaMasivaDetalle.clientnum = item.clientnum.Trim();
                    cargaMasivaDetalle.courier = item.courier;
                    cargaMasivaDetalle.daterep = item.daterep;
                    cargaMasivaDetalle.desp_dias = item.desp_dias;

                    cargaMasivaDetalle.fecha_estimada = item.fecha_estimada;
                    cargaMasivaDetalle.fecha_real_entrega = item.fecha_real_entrega;
                    cargaMasivaDetalle.firstname = item.firstname;
                    cargaMasivaDetalle.guianum = item.guianum;
                    cargaMasivaDetalle.homephone = item.homephone;
                    cargaMasivaDetalle.lastname = item.lastname;
                    cargaMasivaDetalle.numguia = item.numguia;
                    cargaMasivaDetalle.orderdate = item.orderdate;
                    cargaMasivaDetalle.ordernum = item.ordernum;
                    cargaMasivaDetalle.orderdate = item.orderdate;
                    cargaMasivaDetalle.peso = item.peso;

                    cargaMasivaDetalle.postcode = item.postcode;
                    cargaMasivaDetalle.timerep = item.timerep;
                    cargaMasivaDetalle.peso = item.peso;
                    cargaMasivaDetalle.tipo = item.tipo;
                    cargaMasivaDetalle.pesovol = item.pesovol.HasValue == true ? item.pesovol.Value.ToString() : "";
                    cargaMasivaDetalle.bultos = item.bultos.HasValue == true ? item.bultos.Value.ToString() : "";


                    cargaMasivaDetalle.waybillnum = item.waybillnum;
               


                    cargaMasivaDetalles.Add(cargaMasivaDetalle);
                }

                await _context.AddRangeAsync(cargaMasivaDetalles);
                await _context.SaveChangesAsync();



                transaction.Commit();
                

                return cargaMasiva.id;
            }
        }

        public async Task<long> RegistrarOrdenTransporte(IEnumerable<OrdenTrabajoForRegister> ordenes)
        {

                OrdenTrabajo dominio = null;
                GuiaRemisionCliente  guia = null;

                using(var transaction = _context.Database.BeginTransaction())
                {

                    foreach (var command in ordenes)
                    {
                        
                        dominio = new OrdenTrabajo();
                        dominio.idtarifa = command.idtarifa;
                        dominio.dnipersonarecojo = command.dnipersonarecojo;
                        dominio.idcliente = command.idcliente;
                        dominio.idclientetipounidad = command.idclientetipounidad;
                        dominio.idformula = command.idformula;
                        dominio.idconceptocobro = command.idconceptocobro;
                        dominio.iddestinatario = command.iddestinatario;
                        dominio.idusuarioregistro = command.idusuarioregistro;
                        dominio.idorigen = command.idorigen;
                        dominio.iddestino = command.iddestino;
                        dominio.identregara = command.identregara;
                        dominio.idestacionorigen = command.idestacionorigen;
                        dominio.clientnum = command.clientnum;
                        dominio.ordernum = command.ordernum;
                

                        var direccion =  _context.Direcciones.Where(x => x.direccion.Equals(command.direccion)
                             && x.idcliente.Equals(8161)).FirstOrDefault();

                        if (direccion != null)
                            dominio.iddestinatariodireccion = direccion.iddireccion;
                        else
                        {
                            Direccion modDireccion = new Direccion();
                            modDireccion.idcliente = 8161;
                            modDireccion.direccion = command.direccion;
                            modDireccion.iddistrito = command.iddestino ;
                            modDireccion.principal = false;
                            modDireccion.activo = true;

                            _context.Direcciones.Add(modDireccion);
                            _context.SaveChanges();

                            dominio.iddestinatariodireccion = modDireccion.iddireccion;
                        }

                

                        dominio.idtipomercaderia = command.idtipomercaderia;
                        dominio.idtipotransporte = command.idtipotransporte;
                        dominio.personarecojo = command.personarecojo;
                        dominio.telefonorecojo = command.telefonorecojo;

                        dominio.guiarecojo = command.guiarecojo;
                        dominio.guiatercero = command.guiatercero;
                        dominio.idvehiculo = command.idvehiculo;
                        dominio.peso = command.peso;
                        dominio.volumen = command.volumen;
                        if(command.bulto == 0)
                        {
                            dominio.bulto = 1;
                        }
                        else
                        {
                          dominio.bulto = command.bulto;
                        }
                        dominio.docgeneral = command.docgeneral;
                        dominio.iddescripciongeneral = 845;
                        dominio.total = command.total;
                        dominio.subtotal = command.subtotal;
                        dominio.igv = command.igv;
                        dominio.dni = command.dni;
                        dominio.placa = command.placa;
                        dominio.chofer = command.chofer;
                        dominio.conceptocobro = command.conceptocobro;
                        dominio.puntopartida = command.puntopartida;
                        dominio.cepan = command.cepan;
                        dominio.esembarque = command.esembarque;
                        dominio.base1 = command.base1;
                        dominio.tarifa = command.tarifa;
                        dominio.pesovol = command.pesovol;
                        dominio.activo = command.activo;
                        dominio.lineaconsumida = command.subtotal;
                        dominio.fecharecojo = command.fecharecojo;
                        dominio.reintegrotributario = command.reintegrotributario;
                        dominio.registrorapido = command.registrorapido;
                        dominio.clave_consulta = ""; //obtenerClaveConsulta();
                        dominio.idestacioncreacion = command.idestacionorigen;
                        dominio.serie = command.serie;
                        dominio.idremitente = command.idcliente;
                        dominio.idremitentedireccion = 33567;
                        dominio.idestado = command.idestado;
                        dominio.fecharegistro = command.fecharegistro;
                        dominio.idmanifiesto = null;
                        dominio.idcamioncompleto = command.idcamioncompleto;
                        dominio.facturado = false;

                        dominio.idestadocliente = command.idestadocliente;
                        dominio.pesovol = command.pesovol;
                            

                        dominio.subtotalfinal = command.subtotal;  

                        await _context.AddAsync<OrdenTrabajo>(dominio);
                        await _context.SaveChangesAsync();
                        
                        dominio.numcp = "100-" + (dominio.idordentrabajo - 30000).ToString().PadLeft(6, '0');
                        await _context.SaveChangesAsync();


                        foreach (var aux in command.guias)
                        {
                                guia = new  GuiaRemisionCliente();
                        
                                guia.idordentrabajo = dominio.idordentrabajo;
                                guia.nroguia = aux;

                                await _context.AddAsync<GuiaRemisionCliente>(guia);
                                await _context.SaveChangesAsync();
                        }

                     


                       
                    }   
                     transaction.Commit();
                }
                return 1;
        }

        public async Task<IEnumerable<ListarSustentoResult>> GetListarSustentoxHR(string hojaruta)
        {
              using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: hojaruta);
                var resultado = await conn.QueryAsync<ListarSustentoResult>
                    (
                        "seguimiento.pa_listar_detallessustentos",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
                return resultado;
            }
        }

        public async Task<IEnumerable<PendientesLiquidacionResult>> getListarPendientesFacturacionOS(string numhojaruta)
        {
            using (IDbConnection conn = Connection)
            {
                var parametros = new DynamicParameters();
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: numhojaruta);
                var resultado = await conn.QueryAsync<PendientesLiquidacionResult>
                    (
                        "seguimiento.pa_listarpendientefacturacion_ocs",
                        parametros,
                        commandType: CommandType.StoredProcedure
                 );
                return resultado;
            }
        }

        public async Task<Int16> ConfirmarEntrega(InsertarActualizarEtapaCommand command)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                   var dominio =   _context.OrdenTrabajo.Where(x => x.idordentrabajo == command.idordentrabajo).SingleOrDefault();
                   var dominioEtapa = new Etapa();

                    try
                    {
                            //ORDEN
                            dominio.fechaentrega = command.fechaetapa;
                            dominio.idestado = command.idestado;
                            dominio.idusuarioentrega = command.idusuarioentrega;
                            dominio.incidenciaentregaid = command.incidenciaentregaid;
                            dominio.personaentrega = command.recurso;
                            dominio.documentoentrega = command.documento;
                            dominio.canal_confirmacion_id = command.canal_confirmacion_id;
                            dominio.enzona = command.enzona;
                            

                           

                            //ETAPA
                            dominioEtapa.idetapa = null;
                            dominioEtapa.descripcion = command.descripcion;
                            dominioEtapa.fechaetapa = command.fechaetapa;
                            dominioEtapa.fecharegistro = command.fecharegistro;
                            dominioEtapa.idusuarioregistro = command.idusuarioentrega;
                            dominioEtapa.recurso = command.recurso;
                            dominioEtapa.documento = command.documento;
                            dominioEtapa.visible = command.visible;
                            dominioEtapa.idmaestroetapa = command.idtipoentrega;
                            dominioEtapa.idordentrabajo = command.idordentrabajo;
                            dominioEtapa.idtipoentrega = command.idtipoentrega;
                            

                            await _context.Etapas.AddAsync(dominioEtapa);
                            await _context.SaveChangesAsync();

                            scope.Complete();
                        
                    }
                    catch (System.Exception ex)
                    {
                        
                       throw;
                    }

                 



                return 1;

               
            }
          
        }
         public async Task<ObtenerOrdenTrabajoDto> ObtenerCamOrden(string cam)
        {
            using (IDbConnection conn = Connection)
            {
                  var parametros = new DynamicParameters();
                  parametros.Add("cam", dbType: DbType.String, direction: ParameterDirection.Input, value: cam);
                  var result = await conn.QueryAsync<ObtenerOrdenTrabajoDto>("seguimiento.pa_buscarorden_mobile_scan"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result.LastOrDefault();
            }
        }

        public async Task<IEnumerable<OtsRetornoResult>> GetAllOrdenTransporteDocument(int? idcliente, string numcp
        , string fecinicio, string fecfin, string grr
        ,  int? iddestinatario, int? idproveedor)
        {
            var parametros = new DynamicParameters();

            parametros.Add("grr", dbType: DbType.String, direction: ParameterDirection.Input, value: grr);
            parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: numcp);
            parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idcliente);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fecinicio);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fecfin);
            parametros.Add("iddestinatario", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddestinatario);
            parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idproveedor);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[liquidacion].[pa_listarliquidaciondocumentaria]";
                var result = await conn.QueryAsync<OtsRetornoResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.ToList().OrderByDescending(x=>x.fecharecojo);
            }
        }

        public async Task<int> SetCamToOrder(SetCamToOrderForUpdate command)
        {
            var orden =  _context.OrdenTrabajo.Where(x=> x.idordentrabajo == command.idordentrabajo).Single();

            var set_order = _context.OrdenTrabajo.Where(x=> x.cam == command.cam).SingleOrDefault();
            if(set_order != null){
                set_order.cam = null;
            }

          
            orden.cam = command.cam;
            await _context.SaveChangesAsync();

            
            return 1;
        }

        public async Task<bool> ConfirmarRetornoDocumentario(LiquidacionForUpdateDto command)
        {
            
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                   var dominio =   _context.OrdenTrabajo.Where(x => x.idordentrabajo == command.idordentrabajo).SingleOrDefault();
                   var dominioEtapa = new Etapa();

                    try
                    {
                            
                            //OT
                            dominio.idestado = command.idestado;
                            dominio.fechaentregaconciliacion =  command.fechaentregaconciliacion ;
                            dominio.idusuarioconciliacion = command.idusuarioconciliacion;
                            dominio.archivado = true;
                        
                            
                           

                            //ETAPA

                            dominioEtapa.idmaestroetapa = 21; // cargo retornado
                            dominioEtapa.idetapa = null;
                            dominioEtapa.idusuarioregistro = dominio.idusuarioconciliacion;
                            dominioEtapa.visible =true;
                            dominioEtapa.idordentrabajo = command.idordentrabajo;
                            dominioEtapa.fechaetapa = dominio.fechaentregaconciliacion;
                            dominioEtapa.fecharegistro = DateTime.Now; 

                            await _context.Etapas.AddAsync(dominioEtapa);
                            await _context.SaveChangesAsync();

                            scope.Complete();
                        
                    }
                    catch (System.Exception ex)
                    {
                        
                       throw;
                    }

                return true;
               
            }
          
        }
    }
}