using B_LEI.Data;
using B_LEI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace B_LEI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Injeta o contexto do Entity Framework no construtor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Página principal com listagem e filtros
        public IActionResult Index(string search, string categoria)
        {
            // Consulta inicial para obter os livros
            var livros = _context.Livros
        .Include(l => l.Autor) // Inclui o Autor relacionado
        .Include(l => l.Categoria) // Inclui a Categoria relacionada
        .AsQueryable();

            // Filtro por categoria
            if (!string.IsNullOrEmpty(categoria))
            {
                livros = livros.Where(l => l.Categoria != null && l.Categoria.Nome == categoria);
            }

            // Filtro por busca (título ou autor)
            if (!string.IsNullOrEmpty(search))
            {
                livros = livros.Where(l =>
                    (l.Titulo != null && l.Titulo.Contains(search)) ||
                    (l.Autor != null && l.Autor.Nome != null && l.Autor.Nome.Contains(search))
                );
            }


            // Envia as categorias distintas para a View
            ViewBag.Categorias = _context.Livros
                .Select(l => l.Categoria)
                .Distinct()
                .ToList();

            // Retorna os livros filtrados para a View
            return View(livros.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MinhasRequisicoes()
        {
            var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var requisicoes = await _context.Requisicoes
                .Include(r => r.Livro)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(requisicoes);
        }

    }
}
