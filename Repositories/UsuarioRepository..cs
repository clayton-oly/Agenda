using Agenda.Data;
using Agenda.Interfaces;
using Agenda.Models;
using Microsoft.AspNetCore.Connections;
using Npgsql;

namespace Agenda.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AgendaDbConnectionFactory _connectionFactory;

        public UsuarioRepository(AgendaDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Usuario> ListarTodosUsuarios()
        {
            var usuarios = new List<Usuario>();

            var sql = @"SELECT * FROM VW_UsuarioHistorico";

            using var conn = _connectionFactory.CreateConnection();
            using var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var usuario = new Usuario
                {
                    Id_Usuario = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Apelido = reader.IsDBNull(2) ? null : reader.GetString(2),
                    CPF = reader.GetString(3),
                    Email = reader.GetString(4),
                    Telefone = reader.GetString(5),
                    Historico = new Historico
                    {
                        DataCadastro = reader.GetDateTime(6),
                        DataUltimaAlteracao = reader.IsDBNull(7) ? null : reader.GetDateTime(7)
                    }
                };

                usuarios.Add(usuario);
            }

            return usuarios;
        }


        public void InserirUsuario(Usuario usuario)
        {
            using var conn = _connectionFactory.CreateConnection();
            using var cmd = new NpgsqlCommand("CALL sp_inserir_usuario(@nome, @apelido, @cpf, @telefone, @email)", (NpgsqlConnection)conn);

            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@apelido", (object)usuario.Apelido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpf", usuario.CPF);
            cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("@email", usuario.Email);

            cmd.ExecuteNonQuery();
        }

        public Usuario BuscarPorId(int id)
        {
            using var conn = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuario WHERE id_usuario = @id";

            using var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    Id_Usuario = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Apelido = reader.IsDBNull(2) ? null : reader.GetString(2),
                    CPF = reader.GetString(3),
                    Telefone = reader.GetString(4),
                    Email = reader.GetString(5)
                };
            }

            return null;
        }

        public bool BuscarPorCPF(string cpf)
        {
            using var conn = _connectionFactory.CreateConnection();
            string sql = "SELECT FROM Usuario WHERE cpf = @cpf";

            using var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
            cmd.Parameters.AddWithValue("@cpf", cpf);

            using var reader = cmd.ExecuteReader();
            return reader.Read();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            using var conn = _connectionFactory.CreateConnection();
            var sql = @"UPDATE Usuario SET nome = @nome, apelido = @apelido, cpf = @cpf, telefone = @telefone, email = @email WHERE id_usuario = @id";

            using var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@apelido", (object?)usuario.Apelido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpf", usuario.CPF);
            cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("@email", usuario.Email);
            cmd.Parameters.AddWithValue("@id", usuario.Id_Usuario);

            cmd.ExecuteNonQuery();
        }

        public void DeletarUsuario(int id)
        {
            using var conn = _connectionFactory.CreateConnection();
            var sql = "DELETE FROM Usuario WHERE id_usuario = @id";
            using var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}