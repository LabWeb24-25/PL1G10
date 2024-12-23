using B_LEI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B_LEI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // As entidades do teu domínio da biblioteca
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Multa> Multas { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Requisicao> Requisicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Invoca primeiro a configuração base do Identity
            base.OnModelCreating(builder);

            // Se quiseres manter as tabelas com nomes AspNetUsers, AspNetRoles, etc.:
            // Mapeamento do ApplicationUser para "AspNetUsers":
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            // Roles para "AspNetRoles"
            builder.Entity<IdentityRole>().ToTable("AspNetRoles");

            // Se quiseres, mapeia também as outras tabelas do Identity:
            // (Isto é opcional; se não fizeres, ficam com nomes por padrão do EF)
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");

            // Exemplo de seed das Roles:
            var admin = new IdentityRole("admin") { NormalizedName = "ADMIN" };
            var leitor = new IdentityRole("leitor") { NormalizedName = "LEITOR" };
            var bibliotecario = new IdentityRole("bibliotecario") { NormalizedName = "BIBLIOTECARIO" };

            // Tem de chamar HasData para inserir estas roles automaticamente na BD:
            builder.Entity<IdentityRole>().HasData(admin, leitor, bibliotecario);
        }
    }
}
