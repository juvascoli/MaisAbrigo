using System.ComponentModel.DataAnnotations;

namespace MaisAbrigo.DTOs
{
    public class AbrigoDTO
    {
        public string Nome { get; set; }
        [Required(ErrorMessage = "O endereço do abrigo é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "A quantidade de pessoas no abrigo é obrigatória")]
        [Range(0, 2000000, ErrorMessage = "A quantidade de pessoas deve ser no maximo de 2000000")]
        public int OcupacaoAtual { get; set; }
    }
}
