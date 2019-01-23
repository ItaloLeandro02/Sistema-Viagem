using System;
using System.ComponentModel.DataAnnotations.Schema;
using Validation;

namespace api.Models
{
    [Table("viagemDespesa")]
    public class CombustivelDTO
    {
        public int Id { get; set; }
        
        public int ViagemId { get; set; }
        [NotMapped]
        public Viagem viagem { get; set; }

        public DateTimeOffset DataLancamento { get; set; }
        public string Historico { get; set; }
        public double Valor { get; set; }
        public int Tipo { get; set; }

        public void validacoes()
        {
            AssertionConcern.AssertArgumentFalse(this.DataLancamento.Date == new DateTime(0001,01,01).Date, "Data de lançamento é obrigatório!");
            AssertionConcern.AssertArgumentNotEmpty(this.Historico, "Histórico é obrigatório!");
            AssertionConcern.AssertArgumentLength(this.Historico, 5, 60, "Histórico deve conter no mínimo 5 caracteres");
            AssertionConcern.AssertArgumentFalse(this.Valor <= 0, "Valor Deve ser maior que 0!");
            //AssertionConcern.AssertArgumentFalse(this.DataLancamento.Date == viagem.DataChegada.Date, "A data de lançamento deve ser a mesma da chegada");
        }
    }
}