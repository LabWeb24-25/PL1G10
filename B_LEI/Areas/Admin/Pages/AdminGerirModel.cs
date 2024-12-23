using B_LEI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B_LEI.Areas.Admin.Pages
{
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
			AppUser = await _userManager.GetUserAsync(User);
		}
	}

}
