using System.ComponentModel.DataAnnotations;

namespace MaisAbrigo.DTOs
{
    public class PessoaDTO
    {
        [Required(ErrorMessage = "O nome da pessoa é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A idade da pessoa é obrigatória")]
        [Range(0, 120, ErrorMessage = "A idade da pessoa deve ser no maximo de 120 anos")]
        public int Idade { get; set; }
        [Required(ErrorMessage = "O sexo da pessoa é obrigatório")]
        [StringLength(100, ErrorMessage = "O sexo deve ter no máximo 15 caracteres")]
        public string sexo { get; set; }
        public int IdAbrigo { get; set; }
    }
}
