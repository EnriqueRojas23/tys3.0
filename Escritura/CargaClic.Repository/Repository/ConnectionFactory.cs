using System;
using System.Data;
using System.Data.SqlClient;
using CargaClic.Data;
using Microsoft.Extensions.Configuration;

namespace CargaClic.Repository.Repository
{
    public class ConnectionFactory
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public ConnectionFactory(DataContext context,IConfiguration config)
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
        public  IDbConnection CreateFromUserSession()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    conn.Open();
                    return conn;
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }

        }
    }
}