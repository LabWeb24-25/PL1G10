// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using B_LEI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B_LEI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LockoutModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string LockoutReason { get; private set; }

        public async Task OnGetAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                // Buscar o usuário diretamente pelo ID
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null && user.LockoutEnabled)
                {
                    LockoutReason = user.LockoutReason; // Propriedade deve estar preenchida no banco
                }
                else
                {
                    LockoutReason = "Usuário não encontrado ou não está bloqueado.";
                }
            }
            else
            {
                LockoutReason = "ID do usuário não fornecido.";
            }
        }
    }
}
