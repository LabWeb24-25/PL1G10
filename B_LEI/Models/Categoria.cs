namespace B_LEI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        
        public int LivroId { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }

}
