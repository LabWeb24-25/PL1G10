using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B_LEI.Data;
using B_LEI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace B_LEI.Controllers
{
    [Authorize(Roles = "bibliotecario")]
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        public LivrosController(ApplicationDbContext context,
                                IWebHostEnvironment environment,
                                IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = environment;
            _emailSender = emailSender;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editora);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Livros/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editora)
                .FirstOrDefaultAsync(m => m.LivroId == id);

            if (livro == null)
                return NotFound();

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "Nome");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome");
            ViewData["EditoraId"] = new SelectList(_context.Editoras, "EditoraId", "Nome");
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroId,Titulo,ISBN,Edicao,Estado,AnoPublicacao,Capa,Descricao,AutorId,CategoriaId,EditoraId")] LivroViewModel livro)
        {
            // Validar extensões
            var fotoExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(livro.Capa.FileName).ToLower();

            if (!fotoExtensions.Contains(extension))
            {
                ModelState.AddModelError("Foto", "Extensão inválida. Use .jpg, .jpeg ou .png");
            }

            if (ModelState.IsValid)
            {
                var newLivro = new Livro
                {
                    Titulo = livro.Titulo,
                    Capa = livro.Capa.FileName,
                    AnoPublicacao = livro.AnoPublicacao,
                    AutorId = livro.AutorId,
                    CategoriaId = livro.CategoriaId,
                    EditoraId = livro.EditoraId,
                    Descricao = livro.Descricao,
                    Edicao = livro.Edicao,
                    ISBN = livro.ISBN,
                    Estado = livro.Estado
                };

                // Salvar arquivo
                string fotoAutorPath = Path.GetFileName(livro.Capa.FileName);
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "FotoLivro", fotoAutorPath);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await livro.Capa.CopyToAsync(stream);
                }

                _context.Add(newLivro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "AutorId", livro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", livro.CategoriaId);
            ViewData["EditoraId"] = new SelectList(_context.Editoras, "EditoraId", "EditoraId", livro.EditoraId);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                return NotFound();

            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "AutorId", livro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", livro.CategoriaId);
            ViewData["EditoraId"] = new SelectList(_context.Editoras, "EditoraId", "EditoraId", livro.EditoraId);
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroId,Titulo,ISBN,Edicao,AnoPublicacao,Capa,Descricao,AutorId,CategoriaId,EditoraId")] Livro livro)
        {
            if (id != livro.LivroId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "AutorId", livro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", livro.CategoriaId);
            ViewData["EditoraId"] = new SelectList(_context.Editoras, "EditoraId", "EditoraId", livro.EditoraId);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editora)
                .FirstOrDefaultAsync(m => m.LivroId == id);

            if (livro == null)
                return NotFound();

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.LivroId == id);
        }

        // POST: RequisitarLivro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequisitarLivro(int livroId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "É necessário estar autenticado para requisitar um livro." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "Utilizador não encontrado." });
            }

            try
            {
                var livro = await _context.Livros.FindAsync(livroId);
                if (livro == null)
                    return Json(new { success = false, message = "Livro não encontrado." });

                if (!livro.Estado)
                    return Json(new { success = false, message = "Livro já está indisponível." });

                // Atualizar estado do livro
                livro.Estado = false;
                _context.Entry(livro).State = EntityState.Modified;

                // Registrar a requisição
                var requisicao = new Requisicao
                {
                    LivroId = livroId,
                    UserId = userId,
                    DataRequisicao = DateTime.Now
                };
                _context.Requisicoes.Add(requisicao);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Requisição registrada com sucesso!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar requisição: {ex.Message}");
                return Json(new { success = false, message = "Ocorreu um erro ao processar a requisição. Tente novamente." });
            }
        }

        public IActionResult RequisicaoConfirmada()
        {
            ViewData["Message"] = TempData["SuccessMessage"] ?? "Sua requisição foi registrada com sucesso!";
            return View();
        }

        [Authorize(Roles = "bibliotecario")]
        public async Task<IActionResult> ListaRequisicoes()
        {
            // Busca todas as requisições e carrega os dados do livro e do usuário
            var requisicoes = await _context.Requisicoes
                .Include(r => r.Livro)
                .Include(r => r.User)
                .ToListAsync();

            // Identifica as requisições atrasadas (prazo de entrega ultrapassado)
            var hoje = DateTime.Now;
            ViewData["RequisicoesAtrasadas"] = requisicoes
                .Where(r => r.DataEntrega.HasValue && r.DataEntrega.Value < hoje && !r.DataDevolucao.HasValue)
                .Count();

            return View(requisicoes);
        }

    }
}
