using MaisAbrigo.Model;

namespace MaisAbrigo.DTOs
{
    public class AbrigoRespostaDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }
        public string Recursos { get; set; }

        public int OcupacaoAtual { get; set; }
        public List<PessoaResumoDTO> pessoas { get; set; }
    }
}
