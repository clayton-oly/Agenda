using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nome { get; set; }
        public string? Apelido { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Historico? Historico { get; set; }
    }
}


