using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B_LEI.Views.Bibliotecario
{
    [Authorize(Roles = "bibliotecario")]
    public class GerirLivrosBibliotecarioModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
