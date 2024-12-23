namespace B_LEI.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }

        public int LivroId { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }

}
