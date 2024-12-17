using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B_LEI.Views.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminGerirModel : PageModel
    {
        private readonly UserManager<IdentityUser> UserManager;

        public IdentityUser? appUser;

        public AdminGerirModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        public void OnGet()
        {
            var task = UserManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
