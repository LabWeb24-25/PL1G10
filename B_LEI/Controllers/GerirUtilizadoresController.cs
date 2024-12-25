using B_LEI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GerirUtilizadoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GerirUtilizadoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GerirUtilizadoresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GerirUtilizadoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GerirUtilizadoresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GerirUtilizadoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
