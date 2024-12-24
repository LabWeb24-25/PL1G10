using B_LEI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B_LEI.Areas.Admin.Controllers 
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminGerir : Controller
    {
        private ApplicationDbContext _application;
        public AdminGerir(ApplicationDbContext application)
        {
            _application = application;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gerir()
        {
            return View(_application.Users.ToList());
        }
    }
}
