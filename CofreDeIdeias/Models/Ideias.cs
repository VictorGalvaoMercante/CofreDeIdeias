using System.ComponentModel.DataAnnotations;

namespace CofreDeIdeias.Models
{
    public class Ideia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }

        [StringLength(50)]
        public string Categoria { get; set; }

        [Range(1, 5)]
        public int Prioridade { get; set; } = 3;

        public bool Confidencial { get; set; }

        public bool Favorita { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}