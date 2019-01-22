using System.ComponentModel.DataAnnotations;
using Validation;

namespace api.Models
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public byte Desativado { get; set; }

        public void validacoes()
        {
            AssertionConcern.AssertArgumentNotNull(this.Nome, "Nome não pode ser nulo");
            AssertionConcern.AssertArgumentLength(this.Nome, 3, 60, "Nome deve conter no mínimo 3 caracteres");
        }
    }
}