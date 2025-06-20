using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;

namespace Agenda.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public IndexModel(ILogger<IndexModel> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        public List<Usuario> ListaUsuarios { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Filtro { get; set; }

        public void OnGet()
        {
            var usuariosCadastrados = _usuarioRepository.ListarTodosUsuarios();

            if (!string.IsNullOrWhiteSpace(Filtro))
            {
                Filtro = Filtro.ToLower();
                ListaUsuarios = usuariosCadastrados
                    .Where(u =>
                        u.Nome.ToLower().Contains(Filtro) ||
                        u.CPF.Contains(Filtro))
                    .ToList();
            }
            else
            {
                ListaUsuarios = usuariosCadastrados;
            }
        }
    }
}
