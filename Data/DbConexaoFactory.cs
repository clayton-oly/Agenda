using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace Agenda.Data
{
    public class DbConexaoFactory
    {
        private readonly IConfiguration _config;

        public DbConexaoFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Conectar()
        {
            try
            {
                var connection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir conexão com o banco.", ex);
            }

        }
    }
}
