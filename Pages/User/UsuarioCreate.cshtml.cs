using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Interfaces;

namespace Agenda.Pages
{
    public class UsuarioCreate : PageModel
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCreate(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [BindProperty]
        public Models.Usuario Usuario { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Usuario.CPF) || Usuario.CPF.Length != 11)
            {
                TempData["MensagemAlerta"] = "O CPF deve ter exatamente 11 d�gitos.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Usuario.Telefone) || Usuario.Telefone.Length < 10 || Usuario.Telefone.Length > 11)
            {
                TempData["MensagemAlerta"] = "O Telefone deve ter entre 10 e 11 d�gitos.";
                return Page();
            }

            if (!ModelState.IsValid)
                return Page();

            var usuarioExiste = _usuarioRepository.BuscarPorCPF(Usuario.CPF);
            if (usuarioExiste)
            {
                TempData["MensagemAlerta"] = "CPF j� cadastrado!";
                return Page();
            }

            _usuarioRepository.InserirUsuario(Usuario);
            TempData["MensagemSucesso"] = "Usu�rio cadastrado com sucesso!";
            return RedirectToPage("/Index");
        }

    }
}