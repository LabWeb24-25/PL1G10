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

            builder.Entity<Multa>()
               .HasOne(m => m.User)
               .WithMany()
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            // Se quiseres manter as tabelas com nomes AspNetUsers, AspNetRoles, etc.:
            // Mapeamento do ApplicationUser para "AspNetUsers":
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            // Roles para "AspNetRoles"
            builder.Entity<IdentityRole>().ToTable("AspNetRoles");

           
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");


            // Adicionar Categoria
            builder.Entity<Categoria>().HasData(
                new Categoria { CategoriaId = 1, Nome = "Ficção" }
            );
            // Adicionar Autor
            builder.Entity<Autor>().HasData(
                new Autor { AutorId = 1, Nome = "J.R.R. Tolkien" }
            );

            // Adicionar Editora
            builder.Entity<Editora>().HasData(
                new Editora { EditoraId = 1, Nome = "HarperCollins" }
            );

            // Adicionar Livro
            builder.Entity<Livro>().HasData(
                new Livro
                {
                    LivroId = 1,
                    Titulo = "O Senhor dos Anéis",
                    ISBN = "978-3-16-148410-0",
                    Edicao = "1ª",
                    AnoPublicacao = 1954,
                    Capa = "/images/senhor_dos_aneis.png",
                    Descricao = "Um clássico da fantasia.",
                    AutorId = 1, // Relacionamento via ID
                    CategoriaId = 1, // Relacionamento via ID
                    EditoraId = 1 // Relacionamento via ID
                }
                );
        }
    }
}
