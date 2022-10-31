using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CargaClic.Data;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguridad;
using CargaClic.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Common
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(DataContext context,IConfiguration config)
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
                    throw;
                }
            }
        }

        public async Task<User> Get(int usuarioid)
        {
            var parametros = new DynamicParameters();
            parametros.Add("usr_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: usuarioid);

            using (IDbConnection conn = Connection)
            { 
                var resultado = await conn.QueryAsync<User>
                        (
                            "seguridad.pa_obtenerUsuario_1",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        );
                return resultado.SingleOrDefault();
            }
        }

        public async Task<IEnumerable<RolUsuario>> GetRolUsuario(int UserId)
        {
            var parametros = new DynamicParameters();
            parametros.Add("usr_int_id", dbType: DbType.String, direction: ParameterDirection.Input, value: UserId);
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguridad].[pa_listar_rolusuario]";
                var result = await conn.QueryAsync<RolUsuario>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
               
                return result.ToList();
            }
        }

        public async Task<GetUsuario> Login(string username, string password)
        {

            var parametros = new DynamicParameters();
            parametros.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: username);
            parametros.Add("usr_str_password", dbType: DbType.String, direction: ParameterDirection.Input, value: password);
            

            using (IDbConnection conn = Connection)
            {
                string sQuery = "[seguridad].[sp_validarusuario]";
                var result = await conn.QueryAsync<GetUsuario>(sQuery,
                                                                           parametros
                                                                          ,commandType:CommandType.StoredProcedure
                  );
               
                return result.SingleOrDefault();
            }
        }

        public Task<User> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateEstadoId(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}