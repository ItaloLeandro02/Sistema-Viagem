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
            AssertionConcern.AssertArgumentNotEmpty(this.Fabricante, "Nome do fabricante deve conter no mínimo 3 caracteres");
            AssertionConcern.AssertArgumentNotEmpty(this.Modelo, "Modelo não pode ser nullo");
            AssertionConcern.AssertArgumentNotNull(this.AnoModelo, "Ano de modelo obrigatório");
            AssertionConcern.AssertArgumentTrue(this.AnoFabricacao >= 2000, "O veiculo deve ter sido fabricado a partir de 2000");
            AssertionConcern.AssertArgumentTrue(this.AnoModelo >= this.AnoFabricacao, "O ano de fabricação não pode ser maior que o de modelo");
        }
    }
}