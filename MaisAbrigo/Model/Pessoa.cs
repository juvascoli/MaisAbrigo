using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaisAbrigo.Model
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome da pessoa é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A idade da pessoa é obrigatória")]
        [Range(0, 120, ErrorMessage = "A idade da pessoa deve ser no maximo de 120 anos")]
        public int Idade { get; set; }
        public string condicoesSaude { get; set; }
        public int IdAbrigo { get; set; }
        public Abrigo Abrigos { get; set; } 
    }
}
