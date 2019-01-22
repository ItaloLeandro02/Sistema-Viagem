using System.ComponentModel.DataAnnotations;
using Validation;

namespace api.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public byte Desativado { get; set; }

        public void validacoes()
        {
            AssertionConcern.AssertArgumentNotNull(this.AnoFabricacao, "Ano de fasbricação não pode ser nulo");
            AssertionConcern.AssertArgumentNotNull(this.Fabricante, "Nome deve conter no mínimo 3 caracteres");
        }
    }
}