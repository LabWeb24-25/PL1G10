using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using B_LEI.Data;
using B_LEI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace B_LEI.Controllers
{
    [Authorize(Roles = "bibliotecario")]
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LivrosController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Livros.Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Editora);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editora)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroId,Titulo,ISBN,Edicao,AnoPublicacao,Capa,Descricao,AutorId,CategoriaId,EditoraId")] LivroViewModel livro)
        {
            //Validar as extensões dos files
            var FotoExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var extensions = Path.GetExtension(livro.Capa.FileName).ToLower();

            if (!FotoExtensions.Contains(extensions))
            {
                ModelState.AddModelError("Foto", "Extensão inválida. Use .jpg, .jpeg ou .png");
            }
            extensions = Path.GetExtension(livro.Capa.FileName).ToLower();

            if (ModelState.IsValid)
            {
                var newlivro = new Livro();
                newlivro.Titulo = livro.Titulo;
                newlivro.Capa = livro.Capa.FileName;
                newlivro.AnoPublicacao = livro.AnoPublicacao;
                newlivro.AutorId = livro.AutorId;
                newlivro.CategoriaId = livro.CategoriaId;
                newlivro.EditoraId = livro.EditoraId;
                newlivro.Descricao = livro.Descricao;
                newlivro.Edicao = livro.Edicao;
                newlivro.ISBN = livro.ISBN;


                //Salvar file
                string FotoAutorPath = Path.GetFileName(livro.Capa.FileName);
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "FotoLivro", FotoAutorPath);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await livro.Capa.CopyToAsync(stream);
                }

                _context.Add(newlivro);
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
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "AutorId", livro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", livro.CategoriaId);
            ViewData["EditoraId"] = new SelectList(_context.Editoras, "EditoraId", "EditoraId", livro.EditoraId);
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroId,Titulo,ISBN,Edicao,AnoPublicacao,Capa,Descricao,AutorId,CategoriaId,EditoraId")] Livro livro)
        {
            if (id != livro.LivroId)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
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
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editora)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Requisitar(int id)
        {
            // Verifica se o livro existe
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            // Verifica se o livro já está indisponível
            if (!livro.Estado)
            {
                TempData["Mensagem"] = "Este livro já está indisponível.";
                return RedirectToAction("Details", new { id });
            }

            // Obtém o ID do usuário logado
            var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            // Cria uma nova requisição
            var requisicao = new Requisicao
            {
                LivroId = id,
                UserId = userId,
                DataRequisicao = DateTime.Now
            };

            // Marca o livro como indisponível
            livro.Estado = false;

            // Salva as alterações no banco
            _context.Requisicoes.Add(requisicao);
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            // Mensagem de sucesso para o usuário
            TempData["Mensagem"] = "Livro requisitado com sucesso!";
            return RedirectToAction("Details", new { id });
        }


    }
}
