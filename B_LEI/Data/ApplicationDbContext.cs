using B_LEI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B_LEI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Código para adicionar novas roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var leitor = new IdentityRole("leitor");
            leitor.NormalizedName = "leitor";

            var bibliotecario = new IdentityRole("bibliotecario");
            bibliotecario.NormalizedName = "bibliotecario";

            builder.Entity<IdentityRole>().HasData(admin, leitor, bibliotecario);
        }
    }
}
