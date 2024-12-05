using Microsoft.AspNetCore.Identity;


namespace B_LEI.Models
{
    public class Leitor : IdentityUser
    {
        public string Morada { get; set; } = "";
        public DateTime DataCriada { get; set; }

    }
}
