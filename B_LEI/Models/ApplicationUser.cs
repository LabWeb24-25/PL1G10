using Microsoft.AspNetCore.Identity;

namespace B_LEI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // A data em que o registo do user foi criado
        public DateTime DataCriada { get; set; }

        // Morada do utilizador
        public string? Morada { get; set; }
    }
}
