using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CargaClic.Data;
using System.Linq;
using CargaClic.ReadRepository.Contracts.Seguimiento.Results;
using CargaClic.ReadRepository.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using Api.ReadRepository.Contracts.Mantenimiento.Results;
using Api.ReadRepository.Mantenimiento.Parameters;

namespace CargaClic.ReadRepository.Repository.Inventario
{
    public class SeguimientoReadRepository : ISeguimientoReadRepository
    {
            private readonly DataContext _context;
            private readonly IConfiguration _config;

            public SeguimientoReadRepository(DataContext context,IConfiguration config)
            {
                _context = context;
                _config = config;
            }
            public IDbConnection Connection
            {   
                get
                {
                    return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                }
            }

        public async Task<IEnumerable<OrdenesRecojoResult>> GetAllOrdenesRecojo(int? idcliente, string fec_ini, string fec_fin,int? idestado)
        {
             var parametros = new DynamicParameters();
           parametros.Add("idcliente", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idcliente);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            parametros.Add("idestado", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idestado);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listar_ordenesrecojo]";
                conn.Open();
                var result = await conn.QueryAsync<OrdenesRecojoResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<IEnumerable<OrdenesRecojoResult>> GetAllOrdenesTrabajoxIds(string ids)
        {
           var parametros = new DynamicParameters();
            parametros.Add("ids", dbType: DbType.String, direction: ParameterDirection.Input, value: ids);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarordenes_x_ids]";
                conn.Open();
                var result = await conn.QueryAsync<OrdenesRecojoResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<IEnumerable<PlacasProgramadasResult>> GetAllPlacasProgramadas(string ruc, string placa)
        {
            var parametros = new DynamicParameters();
            parametros.Add("ruc", dbType: DbType.String, direction: ParameterDirection.Input, value: ruc);
            parametros.Add("placa", dbType: DbType.String, direction: ParameterDirection.Input, value: placa);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarplacasprogramadas]";
                conn.Open();
                var result = await conn.QueryAsync<PlacasProgramadasResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<IEnumerable<GetAllValorTablaResult>> GetAllValorTabla(int idvalortabla)
        {
            var parametros = new DynamicParameters();
            parametros.Add("idtabla", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idvalortabla);
            parametros.Add("valor", dbType: DbType.String, direction: ParameterDirection.Input, value: null);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarvaloresportabla]";
                conn.Open();
                var result = await conn.QueryAsync<GetAllValorTablaResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }

        public async Task<EquipoTransporteResult> GetEquipoTransporte(string placa)
        {
             var parametros = new DynamicParameters();
             parametros.Add("placa", dbType: DbType.String, direction: ParameterDirection.Input, value: placa);
            
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_getequipotransporte]";
                conn.Open();
                var result = await conn.QueryAsync<EquipoTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.SingleOrDefault();
            }
        }

        public async Task<EquipoTransporteResult> GetEquipoTransporteAsociado(long idor)
        {
                var parametros = new DynamicParameters();
             parametros.Add("idor", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idor);
            
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtener_EquipoTransporteVinculado]";
                conn.Open();
                var result = await conn.QueryAsync<EquipoTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result.SingleOrDefault();
            }
        }

        public async Task<IEnumerable<PorEstadoResult>> GetPorEstado(int? idcliente, int? iddestino)
        {
             var parametros = new DynamicParameters();
             parametros.Add("idcliente", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idcliente);
             parametros.Add("iddestino", dbType: DbType.Int64, direction: ParameterDirection.Input, value: iddestino);
             parametros.Add("idestacion", dbType: DbType.Int64, direction: ParameterDirection.Input, value: null);
            
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_kpi_pendientesEntrega]";
                conn.Open();
                var result = await conn.QueryAsync<PorEstadoResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
                return result;
            }
        }
        public async Task<IEnumerable<GetDespachosATiempo>> GetDespachosATiempo(int? remitente_id, string fec_ini, string fec_fin)
        {
            var parametros = new DynamicParameters();
            parametros.Add("cliente_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: remitente_id);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_despachos_atiempo]";
                conn.Open();
                var result = await conn.QueryAsync<GetDespachosATiempo>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }

        public async Task<IEnumerable<GetEntregaVsConciliacionResult>> GetEntregaVsConciliacion(int? remitente_id, string fec_ini, string fec_fin)
        {
            var parametros = new DynamicParameters();
            parametros.Add("cliente_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: remitente_id);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_despachos_entregavsconciliacion]";
                conn.Open();
                var result = await conn.QueryAsync<GetEntregaVsConciliacionResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }

        public async Task<IEnumerable<GetRetornoDocumentario>> GetPendientesRetornoDocumentario(int? remitente_id
        , string fec_ini, string fec_fin, int? iddepartamento , int? idprovincia )
        {
             var parametros = new DynamicParameters();
            parametros.Add("cliente_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: remitente_id);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idprovincia);
            parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddepartamento);

            using (IDbConnection conn = Connection)
            {
                //string sQuery = "[seguimiento].[pa_despachos_retornodocumentario]";
                string sQuery = "[seguimiento].[pa_pendientes_conciliacion]";
                conn.Open();
                var result = await conn.QueryAsync<GetRetornoDocumentario>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }

        public async Task<IEnumerable<GetRetornoDocumentario>> GetPendientesDespacho(int? remitente_id
        , string fec_ini, string fec_fin, int? iddepartamento , int? idprovincia )
        {
             var parametros = new DynamicParameters();
            parametros.Add("cliente_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: remitente_id);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idprovincia);
            parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddepartamento);

            using (IDbConnection conn = Connection)
            {
                //string sQuery = "[seguimiento].[pa_despachos_retornodocumentario]";
                string sQuery = "[seguimiento].[pa_pendientes_despacho]";
                conn.Open();
                var result = await conn.QueryAsync<GetRetornoDocumentario>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }
 public async Task<IEnumerable<GetRetornoDocumentario>> GetPendientesEntrega(int? remitente_id
        , string fec_ini, string fec_fin, int? iddepartamento , int? idprovincia )
        {
             var parametros = new DynamicParameters();
            parametros.Add("cliente_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: remitente_id);
            parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_ini);
            parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: fec_fin);
            parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idprovincia);
            parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddepartamento);

            using (IDbConnection conn = Connection)
            {
                //string sQuery = "[seguimiento].[pa_despachos_retornodocumentario]";
                string sQuery = "[seguimiento].[pa_pendientes_entregas]";
                conn.Open();
                var result = await conn.QueryAsync<GetRetornoDocumentario>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }

        public async Task<IEnumerable<GetCuadrillaResult>> GetAllCuadrilla(long idrecojo)
        {
               var parametros = new DynamicParameters();
            parametros.Add("idordenrecojo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: idrecojo);

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarcuadrilla]";
                conn.Open();
                var result = await conn.QueryAsync<GetCuadrillaResult>(sQuery,
                                                                    parametros
                                                                    ,commandType:CommandType.StoredProcedure
                  ); 

                return result;
            }
        }

        public async Task<IEnumerable<GetAllTarifarioResult>> GetListarTarifasOrden(ListarTarifaOrdenParameters parameters)
        {
     
            using (IDbConnection conn = Connection)
            {
              var parametros = new DynamicParameters();
                parametros.Add("idorigendistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigendistrito);
                parametros.Add("idorigenprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigenprovincia);
                parametros.Add("idorigendepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigendepartamento);

                parametros.Add("idformula", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idformula);
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                parametros.Add("iddistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddistrito);
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);
                parametros.Add("idconceptocobro", dbType: DbType.Int32, direction: ParameterDirection.Input, value: 117);
                

                var result = await conn.QueryAsync<GetAllTarifarioResult>("seguimiento.pa_traertarifaxorden"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }

        }
        public async Task<IEnumerable<GetAllUbigeoResult>> GetAllUbigeo(string Criterio)
        {
            var parametros = new DynamicParameters();
            parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: Criterio);
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarubigeo]";
                conn.Open();
                var result = await conn.QueryAsync<GetAllUbigeoResult>(sQuery, parametros ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<decimal> GetTarifaProveedor(int? iddestino, int proveedorid, int idtipounidad)
        {
             var parametros = new DynamicParameters();
             parametros.Add("idorigendistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: null );
            parametros.Add("iddestinodistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddestino);
            parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: proveedorid);
            parametros.Add("tipounidadid", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idtipounidad);


            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtenertarifaproveedor]";
                conn.Open();
                var result = await conn.QueryAsync<decimal>(sQuery, parametros ,commandType:CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            
        }
        public async Task<decimal> GetTarifaProveedorProvincia(int? iddestinoprovincia, int proveedorid, int idtipounidad)
        {
             var parametros = new DynamicParameters();
             parametros.Add("idorigendistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: 7 );
            parametros.Add("iddestinoprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: iddestinoprovincia);
            parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: proveedorid);
            parametros.Add("tipounidadid", dbType: DbType.Int32, direction: ParameterDirection.Input, value: idtipounidad);


            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_obtenertarifaproveedor_provincia]";
                conn.Open();
                var result = await conn.QueryAsync<decimal>(sQuery, parametros ,commandType:CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            
        }
    }
}
