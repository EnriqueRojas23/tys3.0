
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CargaClic.Data;
using CargaClic.Repository.Contracts.Seguimiento;
using CargaClic.Repository.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;

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

        public async Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporte(int? idcliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int idusuario)
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

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguimiento].[pa_listarordentrabajo_cliente_v3]";
                var result = await conn.QueryAsync<GetAllOrdenTransporteResult>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
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
    }
}