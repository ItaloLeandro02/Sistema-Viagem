using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class ViagemDespesa
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("viagem")]
        public int ViagemId { get; set; }
        //public virtual Viagem viagem { get; set; }

        public DateTimeOffset DataLancamento { get; set; }
        public string Historico { get; set; }
        public double Valor { get; set; }
        public int Tipo { get; set; }
    }
}