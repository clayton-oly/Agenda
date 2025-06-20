using Agenda.Models;

namespace Agenda.Data
{
    public interface IUsuarioRepository
    {
        List<Usuario> ListarTodosUsuarios();
        Usuario BuscarPorId(int id);
        bool BuscarPorCPF(string cpf);
        void InserirUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        void DeletarUsuario(int id);
    }
}