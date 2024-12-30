using B_LEI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace B_LEI.Controllers
{
    public class ConfirmarEmailsAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ConfirmarEmailsAdminController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> IndexAsync()
        {
            // Busca os usuários com a role "Bibliotecario"
            var bibliotecarios = await _userManager.GetUsersInRoleAsync("Bibliotecario");

            // Retorna para a View com os dados dos Bibliotecários
            return View(bibliotecarios);
        }

        // Aprova um bibliotecário
        [HttpPost]
        public async Task<IActionResult> ApproveBibliotecario(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Marca como aprovado
                user.IsEmailConfirmedByAdmin = true;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Notifica o bibliotecário
                    await _emailSender.SendEmailAsync(user.Email, "Aprovação do E-mail", "Seu e-mail foi aprovado. Você pode agora acessar o site.");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
