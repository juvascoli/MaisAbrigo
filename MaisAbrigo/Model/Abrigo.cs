using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisAbrigo.Model
{
    public class Abrigo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do abrigo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O endereço do abrigo é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; }
        public string Recursos { get; set; }
        [Required(ErrorMessage = "A quantidade de pessoas no abrigo é obrigatória")]
        [Range(0, 2000000, ErrorMessage = "A quantidade de pessoas deve ser no maximo de 2000000")]
        public int OcupacaoAtual { get; set; }
        public ICollection<Abrigo> Abrigos { get; set; }
    }
}
