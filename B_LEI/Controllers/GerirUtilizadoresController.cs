using B_LEI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B_LEI.Controllers
{
    [Authorize(Roles = "admin")]
    public class GerirUtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GerirUtilizadoresController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: GerirUtilizadoresController
        public ActionResult Index()
        {
            // Listar ApplicationUser 
            return View(_context.Users.ToList());
        }

        // GET: GerirUtilizadoresController/Details/5
        public async Task<ActionResult> Details(string id) // Aqui é comum usar string como tipo de id no Identity
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // Ação para bloquear o usuário com um motivo
        public async Task<IActionResult> Bloquear(string id, string motivo)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Bloquear o usuário definindo LockoutEnd para 100 anos no futuro
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);

            // Definir o motivo do bloqueio
            user.LockoutReason = motivo;

            _context.Update(user);
            await _context.SaveChangesAsync();

            // Redirecionar de volta para a página de detalhes do usuário
            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        // Ação para desbloquear o usuário
        public async Task<IActionResult> Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Remover o bloqueio definindo LockoutEnd como null
            user.LockoutEnd = null;
            user.LockoutReason = null; // Limpar o motivo de bloqueio

            _context.Update(user);
            await _context.SaveChangesAsync();

            // Redirecionar de volta para a página de detalhes do usuário
            return RedirectToAction(nameof(Details), new { id = user.Id });
        }
    }
}
