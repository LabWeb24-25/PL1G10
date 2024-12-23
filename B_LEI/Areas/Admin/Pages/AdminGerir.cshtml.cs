using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using B_LEI.Models; // <-- namespace onde vive a tua classe ApplicationUser

namespace B_LEI.Areas.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminGerirModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser? AppUser { get; set; }

        public AdminGerirModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            // Obtém o utilizador atual (cast do HttpContext.User)
            AppUser = await _userManager.GetUserAsync(User);
        }
    }
}
