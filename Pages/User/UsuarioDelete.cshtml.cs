using Agenda.Interfaces;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agenda.Pages.User
{
    public class UsuarioDelete : PageModel
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDelete(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public IActionResult OnGet(int id)
        {
            Usuario = _usuarioRepository.BuscarPorId(id);

            if (Usuario == null)
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Usuario == null || Usuario.Id_Usuario == 0)
                return RedirectToPage("/Index");

            _usuarioRepository.DeletarUsuario(Usuario.Id_Usuario);
            TempData["MensagemSucesso"] = "Usuário deletado com sucesso!";

            return RedirectToPage("/Index");
        }
    }
}