using Agenda.Data;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agenda.Pages.User
{
    public class UsuarioEdit : PageModel

    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioEdit(IUsuarioRepository usuarioRepository)
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
            if (string.IsNullOrWhiteSpace(Usuario.CPF) || Usuario.CPF.Length != 11)
            {
                TempData["MensagemAlerta"] = "O CPF deve ter exatamente 11 dígitos.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Usuario.Telefone) || Usuario.Telefone.Length < 10 || Usuario.Telefone.Length > 11)
            {
                TempData["MensagemAlerta"] = "O Telefone deve ter entre 10 e 11 dígitos.";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _usuarioRepository.AtualizarUsuario(Usuario);

            TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
            return RedirectToPage("/Index");
        }
    }
}